using System;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Volo.Abp.TestApp.Domain
{
    public class Person : AggregateRoot<Guid>, IMultiTenant, ISoftDelete
    {
        public virtual Guid? TenantId { get; set; }

        public virtual string Name { get; private set; }

        public virtual int Age { get; set; }

        public virtual Collection<Phone> Phones { get; set; }

        public bool IsDeleted { get; set; }

        private Person()
        {
            
        }

        public Person(Guid id, string name, int age, Guid? tenantId = null)
        {
            Id = id;
            Name = name;
            Age = age;
            TenantId = tenantId;

            Phones = new Collection<Phone>();
        }

        public void ChangeName(string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var oldName = Name;
            Name = name;
            DomainEvents.Add(new PersonNameChangedEvent{Person = this, OldName =  oldName});
        }
    }
}