using Xunit;

namespace Allors.Dynamic.Tests
{
    public class UnitTests
    {
        [Fact]
        public void PropertySet()
        {
            var population = new DynamicPopulation(v => v
                 .AddUnitRelation("FirstName")
              );

            dynamic jubayer = population.Create();
            dynamic walter = population.Create();

            jubayer.FirstName = "Jubayer";
            walter.FirstName = "Walter";

            Assert.Equal("Jubayer", jubayer.FirstName);
            Assert.Equal("Walter", walter.FirstName);
        }


        [Fact]
        public void IndexSet()
        {
            var population = new DynamicPopulation(v => v
                 .AddUnitRelation("FirstName")
              );

            dynamic jubayer = population.Create();
            dynamic walter = population.Create();

            jubayer.FirstName = "Jubayer";
            walter.FirstName = "Walter";

            Assert.Equal("Jubayer", jubayer.FirstName);
            Assert.Equal("Walter", walter.FirstName);
        }
    }
}
