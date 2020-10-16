namespace csOdin.LinqUtils.Tests.Models
{
    using System;

    public class Person
    {
        public Address Address { get; set; }

        public Guid Id { get; } = Guid.NewGuid();

        public string Name { get; set; }
        public string Surname { get; set; }
    }
}