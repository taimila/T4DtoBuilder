using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    /// <summary>
    /// Example of how to extend generated builder API. This class is completely
    /// optional.
    /// </summary>
    public partial class PersonDtoBuilder
    {
        /// <summary>
        /// Default values for PersonDto. These are always set to PersonDto
        /// created with a builder. You can override any value with 
        /// WithPropertyName() methods.
        /// </summary>
        protected override void Defaults()
        {
            dto.FirstName = "John";
            dto.LastName = "Doe";
            dto.Age = 30;
        }

        /// <summary>
        /// Example of custom method. This extends PersonDtoBuilder with
        /// new With-method that allow to set the whole name with one call.
        /// </summary>
        public PersonDtoBuilder WithFullname(string fullname)
        {
            dto.FirstName = fullname.Split(' ')[0];
            dto.LastName = fullname.Split(' ')[1];
            return this;
        }
    }
}
