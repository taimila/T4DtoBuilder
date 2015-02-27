using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production
{
    public class GroupDto
    {
        public string Name { get; set; }
        public List<PersonDto> Members { get; set; }
    }

    public class PersonDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public Color FavoriteColor { get; set; }
        public AddressDto Address { get; set; }
    }

    public class AddressDto
    {
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
    }

    public enum Color
    {
        Red,
        Green,
        Blue
    }
}
