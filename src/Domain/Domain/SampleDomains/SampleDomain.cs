using CleanArchitectureTemplate.Domain.SampleDomains.Enums;
using CleanArchitectureTemplate.Domain.SampleDomains.Events;
using CleanArchitectureTemplate.Domain.SampleDomains.Rules;
using CleanArchitectureTemplate.Domain.BuildingBlocks.BaseTypes;

namespace CleanArchitectureTemplate.Domain.SampleDomains
{
    public class SampleDomain : DomainEntity<int>
    {
        #region Constructors

        private SampleDomain()
        {
        }

        public SampleDomain(string name, SampleDomainStatus status, string description, int actionUserId) : base(actionUserId)
        {
            SetValues(name, status, description);
        }

        #endregion

        #region Members

        public string Name { get; private set; }
        
        public SampleDomainStatus Status { get; private set; }
        
        public string Description { get; private set; }

        #endregion

        #region Behaviour

        public void Update(string name, SampleDomainStatus status, string description, int actionUserId)
        {
            SetValues(name, status, description);
            LogModification(actionUserId);
        }

        #endregion

        #region Private Methods

        private void SetValues(string name, SampleDomainStatus status, string description)
        {
            CheckRule(new SampleDomainNameRequiredRule(name));

            Name = name;
            Status = status;
            Description = description;

            if (Status == SampleDomainStatus.Active)
                AddDomainEvent(new SampleDomainActivatedEvent(this));
        }

        #endregion
    }
}
