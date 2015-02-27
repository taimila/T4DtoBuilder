using System;
using System.Linq;

namespace Tests
{
    public class Build
    {
		public static GroupDtoBuilder GroupDto { get { return new GroupDtoBuilder(); } }
		public static PersonDtoBuilder PersonDto { get { return new PersonDtoBuilder(); } }
		public static AddressDtoBuilder AddressDto { get { return new AddressDtoBuilder(); } }
    }

	public abstract class Builder<TDto>
    {
        protected TDto dto;

        public Builder()
        {
            dto = Activator.CreateInstance<TDto>();
			Defaults();
        }

		protected virtual void Defaults() {}
        
        public TDto Build()
        {
            return dto;
        }
    }
	
	public partial class GroupDtoBuilder : Builder<Production.GroupDto>
	{
    	public GroupDtoBuilder WithName(System.String name)
		{
			dto.Name = name;
			return this;
		}
		public GroupDtoBuilder WithMembers(System.Collections.Generic.List<Production.PersonDto> members)
		{
			dto.Members = members;
			return this;
		}
		
		public GroupDtoBuilder WithMembers(params Production.PersonDto[] members)
		{
			dto.Members = members.ToList();
			return this;
		}
		
		public GroupDtoBuilder WithMembers(params Action<PersonDtoBuilder>[] builders)
		{
			var members = new System.Collections.Generic.List<Production.PersonDto>();

			foreach(var builder in builders)
            {
                var b = new PersonDtoBuilder();
                builder.Invoke(b);
                members.Add(b.Build());
            }

            dto.Members  = members;

			return this;
		}
	}

	public partial class PersonDtoBuilder : Builder<Production.PersonDto>
	{
    	public PersonDtoBuilder WithFirstName(System.String firstname)
		{
			dto.FirstName = firstname;
			return this;
		}
		public PersonDtoBuilder WithLastName(System.String lastname)
		{
			dto.LastName = lastname;
			return this;
		}
		public PersonDtoBuilder WithAge(System.Int32 age)
		{
			dto.Age = age;
			return this;
		}
		public PersonDtoBuilder WithFavoriteColor(Production.Color favoritecolor)
		{
			dto.FavoriteColor = favoritecolor;
			return this;
		}
		public PersonDtoBuilder WithAddress(Production.AddressDto address)
		{
			dto.Address = address;
			return this;
		}
	
		public PersonDtoBuilder WithAddress(Action<AddressDtoBuilder> addressBuilder)
		{
			var b = new AddressDtoBuilder();
            addressBuilder.Invoke(b);
            dto.Address = b.Build();
            return this;
		}
	}

	public partial class AddressDtoBuilder : Builder<Production.AddressDto>
	{
    	public AddressDtoBuilder WithStreet(System.String street)
		{
			dto.Street = street;
			return this;
		}
		public AddressDtoBuilder WithZipCode(System.String zipcode)
		{
			dto.ZipCode = zipcode;
			return this;
		}
		public AddressDtoBuilder WithCity(System.String city)
		{
			dto.City = city;
			return this;
		}
	}
}
