﻿namespace csOdin.LinqUtils.Tests.Models
{
    using System.Collections.Generic;

    public static class DummyData
    {
        public static List<Person> GetCoders() => new List<Person>()
        {
            new Person()
            {
                Name = "Coder 1 name",
                Surname  = "CoderSur 1 name",
                IntProperty = 1970,
                DecimalProperty = 1970.1M,
                BoolProperty = true,
                Address = new Address()
                {
                    Street = "Street AC",
                    City = "City A",
                },
            },
            new Person()
            {
                Name = "Coder 2 name",
                Surname  = "CoderSur 2 surname",
                IntProperty = 1980,
                DecimalProperty = 1980.1M,
                BoolProperty = true,
                Address = new Address()
                {
                    Street = "Street BC",
                    City = "City B",
                },
            },
            new Person()
            {
                Name = "Coder 3 name",
                Surname  = "CoderSur 3 surname",
                IntProperty = 1990,
                DecimalProperty = 1990.1M,
                BoolProperty = false,
                Address = new Address()
                {
                    Street = "Street CC",
                    City = "City C",
                },
            },
        };

        public static List<Person> GetPeople()
        {
            var p = new List<Person>();
            p.AddRange(GetPerson());
            p.AddRange(GetUsers());
            p.AddRange(GetCoders());
            return p;
        }

        public static List<Person> GetPerson() => new List<Person>()
        {
            new Person()
            {
                Name = "Person 1 name",
                Surname  = "PersonSur 1 name",
                IntProperty = 1970,
                DecimalProperty = 1970.2M,
                BoolProperty = true,
                Address = new Address()
                {
                    Street = "Street AA",
                    City = "City A",
                },
            },
            new Person()
            {
                Name = "Person 2 name",
                Surname  = "PersonSur 2 name",
                IntProperty = 1980,
                DecimalProperty = 1980.2M,
                BoolProperty = true,
                Address = new Address()
                {
                    Street = "Street BA",
                    City = "City B",
                },
            },
            new Person()
            {
                Name = "Person 3 name",
                Surname  = "PersonSur 3 name",
                IntProperty = 1990,
                DecimalProperty = 1990.2M,
                BoolProperty = false,
                Address = new Address()
                {
                    Street = "Street CA",
                    City = "City C",
                },
            },
        };

        public static List<Person> GetUsers() => new List<Person>()
        {
            new Person()
            {
                Name = "User 1 name",
                Surname  = "UserSur 1 name",
                IntProperty = 1970,
                DecimalProperty = 1970.3M,
                BoolProperty = true,
                Address = new Address()
                {
                    Street = "Street AB",
                    City = "City A",
                },
            },
            new Person()
            {
                Name = "User 2 name",
                Surname  = "UserSur 2 name",
                IntProperty = 1980,
                DecimalProperty = 1980.3M,
                BoolProperty = true,
                Address = new Address()
                {
                    Street = "Street BB",
                    City = "City B",
                },
            },
            new Person()
            {
                Name = "User 3 name",
                Surname  = "UserSur 3 name",
                IntProperty = 1990,
                DecimalProperty = 1990.3M,
                BoolProperty = false,
                Address = new Address()
                {
                    Street = "Street CB",
                    City = "City C",
                },
            },
        };
    }
}