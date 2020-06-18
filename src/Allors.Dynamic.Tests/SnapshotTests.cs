namespace Allors.Dynamic.Tests
{
    using Xunit;

    public class SnapshotTests
    {
        [Fact]
        public void Snapshot()
        {
            DynamicPopulation population = new DynamicPopulation(v =>
            {
                v.AddUnit("FirstName");
                v.AddUnit("LastName");
            });

            dynamic john = population.New();
            dynamic jane = population.New();

            john.FirstName = "John";
            john.LastName = "Doe";

            DynamicChangeSet snapshot1 = population.Snapshot();

            jane.FirstName = "Jane";
            jane.LastName = "Doe";

            System.Collections.Generic.Dictionary<DynamicObject, object> changedFirstNames = snapshot1.ChangedRoles("FirstName");
            System.Collections.Generic.Dictionary<DynamicObject, object> changedLastNames = snapshot1.ChangedRoles("LastName");

            Assert.Single(changedFirstNames.Keys);
            Assert.Single(changedLastNames.Keys);
            Assert.Contains(john, changedFirstNames.Keys);
            Assert.Contains(john, changedLastNames.Keys);

            DynamicChangeSet snapshot2 = population.Snapshot();

            changedFirstNames = snapshot2.ChangedRoles("FirstName");
            changedLastNames = snapshot2.ChangedRoles("LastName");

            Assert.Single(changedFirstNames.Keys);
            Assert.Single(changedLastNames.Keys);
            Assert.Contains(jane, changedFirstNames.Keys);
            Assert.Contains(jane, changedLastNames.Keys);
        }
    }
}
