﻿namespace Allors.Dynamic.Meta
{
    public class DynamicOneToOneRoleType : IDynamicToOneRoleType
    {
        public DynamicOneToOneRoleType(DynamicObjectType objectType, string singularName)
        {
            var pluralizer = objectType.Meta.Pluralizer;

            this.ObjectType = objectType;
            this.SingularName = singularName ?? objectType.Type.Name;
            this.PluralName = pluralizer.Pluralize(this.SingularName);
        }

        public DynamicObjectType ObjectType { get; }

        IDynamicAssociationType IDynamicRoleType.AssociationType => this.AssociationType;

        public DynamicOneToOneAssociationType AssociationType { get; internal set; }

        public string Name => this.SingularName;

        public string SingularName { get; }

        public string PluralName { get; }

        public bool IsOne => true;

        public bool IsMany => false;

        public bool IsUnit => false;

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.Name;
        }

        public void Deconstruct(out DynamicOneToOneAssociationType associationType, out DynamicOneToOneRoleType roleType)
        {
            associationType = this.AssociationType;
            roleType = this;
        }

        public object Normalize(object value) => this.NormalizeToOne(value);
    }
}