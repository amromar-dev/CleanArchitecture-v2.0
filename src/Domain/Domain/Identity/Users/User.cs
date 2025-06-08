using Microsoft.AspNetCore.Identity;
using CleanArchitectureTemplate.Domain.BuildingBlocks.Interfaces;
using CleanArchitectureTemplate.Domain.BuildingBlocks.ValueTypes;
using CleanArchitectureTemplate.SharedKernels.Exceptions;
using CleanArchitectureTemplate.SharedKernels.Localizations;
using CleanArchitectureTemplate.Domain.Identity.Users.Enums;

namespace CleanArchitectureTemplate.Domain.Identity.Users
{
    public class User : IdentityUser<int>, IEntity<int>
    {
        #region Constructors

        protected User()
        {

        }

        public User(string username, string name, string profileImage, Phone phone, int actionUserId)
        {
            UserName = username;
            Email = username;
            EmailConfirmed = true;
            FullName = name;
            ProfileImage = profileImage;
            Phone = phone;
            Status = UserStatus.Active;
            Auditing = new Auditing(actionUserId);
        }

        #endregion

        #region Properties

        public UserStatus Status { get; private set; }

        public string FullName { get; private set; }

        public string ProfileImage { get; private set; }

        public DateTime? LastLoginAt { get; private set; }

        public Phone Phone { get; private set; }

        public Auditing Auditing { get; private set; }

        private readonly List<UserRole> _userRoles = [];
        public IReadOnlyList<UserRole> UserRoles => _userRoles.AsReadOnly();
        public List<string> Roles => _userRoles.Select(s => s.Role?.Name).ToList();

        private readonly List<UserClaim> _userClaims = [];
        public IReadOnlyList<UserClaim> UserClaims => _userClaims.AsReadOnly();

        public override string PhoneNumber { get => Phone?.Number; set => throw new InvalidDataException(); }

        #endregion

        #region Behaviors

        public void ChangeData(string fullName, string profileImage, Phone phone, int actionUserId)
        {
            FullName = fullName;
            ProfileImage = profileImage;
            Phone = phone;
            Auditing.LogModification(actionUserId);
        }

        public void ChangeStatus(UserStatus status, int actionUserId)
        {
            Status = status;
            Auditing.LogModification(actionUserId);
        }

        public virtual void LogLastLoginDate()
        {
            if (Status != UserStatus.Active)
                throw new BusinessRuleException(Localization.InvalidOperationException);

            LastLoginAt = DateTime.UtcNow;
        }

        public virtual bool IsActive()
        {
            return Status == UserStatus.Active;
        }

        public void Delete(int actionUserId)
        {
            Auditing.Delete(actionUserId);

        }

        public void Restore(int actionUserId)
        {
            Auditing.Restore(actionUserId);
        }

        public virtual UserType GetUserType() => UserType.SystemAdmin;

        protected void AddClaims(string claimType, string claimValue)
        {
            _userClaims.RemoveAll(x => x.ClaimType == claimType);
            _userClaims.Add(new UserClaim(claimType, claimValue));
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Validate on business rule
        /// </summary>
        /// <param name="rule"></param>
        /// <exception cref="BusinessException"></exception>
        protected static void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
                throw new BusinessRuleException(rule.Message);
        }

        #endregion
    }
}
