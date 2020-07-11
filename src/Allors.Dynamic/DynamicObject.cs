﻿namespace Allors.Dynamic
{
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using Allors.Dynamic.Meta;

    public abstract class DynamicObject : System.Dynamic.DynamicObject
    {
        private readonly IDynamicPopulation population;

        protected DynamicObject(IDynamicPopulation population)
        {
            this.population = population;
        }

        /// <inheritdoc/>
        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            return this.population.TryGetIndex(this, binder, indexes, out result);
        }

        /// <inheritdoc/>
        public override bool TrySetIndex(SetIndexBinder binder, object[] indexes, object value)
        {
            return this.population.TrySetIndex(this, binder, indexes, value);
        }

        /// <inheritdoc/>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return this.population.TryGetMember(this, binder, out result);
        }

        /// <inheritdoc/>
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            return this.population.TrySetMember(this, binder, value);
        }

        /// <inheritdoc/>
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            return this.population.TryInvokeMember(this, binder, args, out result);
        }

        /// <inheritdoc/>
        public override IEnumerable<string> GetDynamicMemberNames()
        {
            var objectType = this.population.Meta.ObjectTypeByType[this.GetType()];
            foreach (IDynamicRoleType roleType in objectType.RoleTypeByName.Values.ToArray().Distinct())
            {
                yield return roleType.Name;
            }

            foreach (IDynamicAssociationType associationType in objectType.AssociationTypeByName.Values.ToArray().Distinct())
            {
                yield return associationType.Name;
            }
        }

        public T GetRole<T>(string name) => this.population.GetRole<T>(this, name);

        public void SetRole<T>(string name, T value) => this.population.SetRole<T>(this, name, value);

        public T GetAssociation<T>(string name) => this.population.GetAssociation<T>(this, name);
    }
}