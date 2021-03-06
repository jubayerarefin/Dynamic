namespace Allors.Dynamic.Tests
{
    using System;
    using System.Linq;
    using Allors.Dynamic.Meta;
    using Allors.Dynamic.Tests.Domain;
    using Xunit;

    public class DerivationOverrideTests
    {
        [Fact]
        public void Derivation()
        {
            var population = new Default.DynamicPopulation(
                new DynamicMeta(new Pluralizer()),
                v =>
            {
                v.AddUnit<Person, string>("FirstName");
                v.AddUnit<Person, string>("LastName");
                v.AddUnit<Person, string>("FullName");
                v.AddUnit<Person, DateTime>("DerivedAt");
                v.AddUnit<Person, string>("Greeting");
            });

            population.DerivationById["FullName"] = new FullNameDerivation();
            population.DerivationById["Greeting"] = new GreetingDerivation();

            dynamic john = population.New<Person>();
            john.FirstName = "John";
            john.LastName = "Doe";

            population.Derive();

            Assert.Equal("Hello John Doe!", john.Greeting);
        }

        public class FullNameDerivation : IDynamicDerivation
        {
            public void Derive(DynamicChangeSet changeSet)
            {
                System.Collections.Generic.Dictionary<DynamicObject, object> firstNames = changeSet.ChangedRoles<Person>("FirstName");
                System.Collections.Generic.Dictionary<DynamicObject, object> lastNames = changeSet.ChangedRoles<Person>("LastName");

                if (firstNames?.Any() == true || lastNames?.Any() == true)
                {
                    System.Collections.Generic.IEnumerable<DynamicObject> people = firstNames.Union(lastNames).Select(v => v.Key).Distinct();

                    foreach (dynamic person in people)
                    {
                        // Dummy updates ...
                        person.FirstName = person.FirstName;
                        person.LastName = person.LastName;

                        person.DerivedAt = DateTime.Now;

                        person.FullName = $"{person.FirstName} {person.LastName}";
                    }
                }
            }
        }

        public class GreetingDerivation : IDynamicDerivation
        {
            public void Derive(DynamicChangeSet changeSet)
            {
                System.Collections.Generic.Dictionary<DynamicObject, object> fullNames = changeSet.ChangedRoles<Person>("FullName");

                if (fullNames?.Any() == true)
                {
                    System.Collections.Generic.IEnumerable<DynamicObject> people = fullNames.Select(v => v.Key).Distinct();

                    foreach (dynamic person in people)
                    {
                        person.Greeting = $"Hello {person.FullName}!";
                    }
                }
            }
        }
    }
}
