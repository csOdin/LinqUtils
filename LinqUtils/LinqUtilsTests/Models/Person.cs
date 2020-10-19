namespace csOdin.LinqUtils.Tests.Models
{
    using System;

    public class Person
    {
        public Address Address { get; set; }

        public bool BoolProperty { get; set; }
        public decimal DecimalProperty { get; set; }
        public Guid Id { get; } = Guid.NewGuid();

        public int IntProperty { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}