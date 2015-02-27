using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Production;
using System.Collections.Generic;

namespace Tests
{
    /// <summary>
    /// These tests serve as an examples how to utilize generated
    /// builder classes.
    /// </summary>
    [TestClass]
    public class BuilderTests
    {
        [TestMethod]
        public void BasicAPI()
        {
            var person = Build.PersonDto.WithAge(22)
                                        .WithFavoriteColor(Color.Red)
                                        .Build();

            Assert.AreEqual(22, person.Age);
            Assert.AreEqual(Color.Red, person.FavoriteColor);
        }

        [TestMethod]
        public void NestedSingleDto()
        {
            var address = Build.AddressDto.WithStreet("Address")
                                          .WithZipCode("2424")
                                          .WithCity("New York")
                                          .Build();

            var person = Build.PersonDto.WithAddress(address).Build();

            Assert.AreEqual("New York", person.Address.City);
        }

        [TestMethod]
        public void NestedSingleDto_DefinedInPlaceAPI()
        {
            var person = Build.PersonDto.WithFirstName("Thomas")
                                        .WithAge(23)
                                        .WithAddress(a => a.WithStreet("Address")
                                                           .WithZipCode("2424")
                                                           .WithCity("New York"))
                                        .Build();

            Assert.AreEqual(23, person.Age);
            Assert.AreEqual("New York", person.Address.City);
        }

        [TestMethod]
        public void NestedMultipleDtos()
        {
            var p1 = Build.PersonDto.WithFirstName("First").Build();
            var p2 = Build.PersonDto.WithFirstName("Second").Build();
            var p3 = Build.PersonDto.WithFirstName("Third").Build();
            var members = new List<PersonDto> { p1, p2, p3};

            var group = Build.GroupDto.WithName("Winners")
                                      .WithMembers(members).Build();

            Assert.AreEqual("Winners", group.Name);
            Assert.AreEqual("First", group.Members[0].FirstName);
            Assert.AreEqual("Second", group.Members[1].FirstName);
            Assert.AreEqual("Third", group.Members[2].FirstName);
        }

        [TestMethod]
        public void NestedMultipleDtos_ParamsAPI()
        {
            var p1 = Build.PersonDto.WithFirstName("First").Build();
            var p2 = Build.PersonDto.WithFirstName("Second").Build();
            var p3 = Build.PersonDto.WithFirstName("Third").Build();

            var group = Build.GroupDto.WithName("Winners")
                                      .WithMembers(p1, p2, p3)
                                      .Build();

            Assert.AreEqual("Winners", group.Name);
            Assert.AreEqual("First", group.Members[0].FirstName);
            Assert.AreEqual("Second", group.Members[1].FirstName);
            Assert.AreEqual("Third", group.Members[2].FirstName);
        }

        [TestMethod]
        public void NestedMultipleDtos_DefinedInPlaceAPI()
        {
            var group = Build.GroupDto.WithName("Winners")
                                      .WithMembers(p1 => p1.WithFirstName("First"), 
                                                   p2 => p2.WithFirstName("Second"), 
                                                   p3 => p3.WithFirstName("Third"))
                                      .Build();

            Assert.AreEqual("Winners", group.Name);
            Assert.AreEqual("First", group.Members[0].FirstName);
            Assert.AreEqual("Second", group.Members[1].FirstName);
            Assert.AreEqual("Third", group.Members[2].FirstName);
        }

        [TestMethod]
        public void NestingWorksOnMultipleLevels()
        {
            var group = Build.GroupDto.WithMembers(p => p.WithFirstName("Thomas")
                                                         .WithLastName("Anderson")
                                                         .WithAddress(a => a.WithStreet("Streetname")
                                                                            .WithCity("Boston")))
                                      .Build();

            Assert.AreEqual("Thomas", group.Members[0].FirstName);
            Assert.AreEqual("Boston", group.Members[0].Address.City);
        }

        [TestMethod]
        public void DefaultValues()
        {
            // See PersonDtoBuilder.cs
            var person = Build.PersonDto.WithFirstName("Thomas").Build();

            Assert.AreEqual("Thomas", person.FirstName);
            Assert.AreEqual("Doe", person.LastName);
            Assert.AreEqual(30, person.Age);
        }

        [TestMethod]
        public void CustomMethod()
        {
            // See PersonDtoBuilder.cs
            var person = Build.PersonDto.WithFullname("Michael Jackson").Build();

            Assert.AreEqual("Michael", person.FirstName);
            Assert.AreEqual("Jackson", person.LastName);
        }
    }
}
