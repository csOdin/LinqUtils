namespace csOdin.LinqUtils.Tests.OrderByTests
{
    using csOdin.LinqUtils.OrderBy;
    using System.Linq;
    using Xunit;

    public class OrderByTests
    {
        [Theory]
        [InlineData("Name")]
        [InlineData("Name desc")]
        [InlineData("Address.City, Name desc")]
        [InlineData("Address.City desc, Name")]
        [InlineData("Address.City desc, Name desc")]
        [InlineData("")]
        [InlineData(null)]
        public void StringShouldReturn(string inputString)
        {
            var people = Models.DummyData.GetPeople().AsQueryable();

            var orderedPeople = people.OrderBy(inputString);
        }
    }
}