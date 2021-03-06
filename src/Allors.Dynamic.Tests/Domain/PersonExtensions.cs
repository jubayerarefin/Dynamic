namespace Allors.Dynamic.Tests.Domain
{
    public static class PersonExtensions
    {
        public static string Name(this Person @this) => (string)@this.GetRole(nameof(Name));

        public static Person Name(this Person @this, string value)
        {
            @this.SetRole(nameof(Name), value);
            return @this;
        }

        public static Organisation OrganisationWhereOwner(this Person @this) => (Organisation)@this.GetAssociation(nameof(OrganisationWhereOwner));
    }
}
