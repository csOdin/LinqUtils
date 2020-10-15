namespace csOdin.LinqUtilsTests.Models
{
    using System;

    public class Address
    {
        public string City { get; set; }

        public Guid Id { get; } = Guid.NewGuid();

        public string Street { get; set; }
    }
}