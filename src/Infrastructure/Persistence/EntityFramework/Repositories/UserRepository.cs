using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CleanArchitectureTemplate.Domain.BuildingBlocks.BaseTypes;
using CleanArchitectureTemplate.SharedKernels.Exceptions;
using CleanArchitectureTemplate.SharedKernels.Localizations;
using System.Data;
using CleanArchitectureTemplate.Domain.Identity.Users;
using CleanArchitectureTemplate.Domain.Identity.Users.Enums;
using CleanArchitectureTemplate.Domain.Identity.Users.Interfaces;

namespace CleanArchitectureTemplate.Infrastructure.Persistence.EntityFramework.Repositories
{
    public class UserRepository(ApplicationDbContext dbContext, UserManager<User> userManager) : IUserRepository 
    {
        protected readonly ApplicationDbContext dbContext = dbContext;
        protected readonly DbSet<User> dbSet = dbContext.Set<User>();

        public Task<List<User>> ListAsync()
        {
            return dbSet.ToListAsync();
        }

        public virtual Task<User> FindAsync(int id)
        {
            return dbSet.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<User> GetByUserNameAsync(string userName, bool includeDeleted = false)
        {
            string normalizedUsername = Normalized(userName);
            IQueryable<User> query = dbSet.Include(e => e.UserClaims).AsQueryable();

            if (includeDeleted)
                query = query.IgnoreQueryFilters();

            return query.Where(u => u.NormalizedUserName == normalizedUsername).FirstOrDefaultAsync();
        }

        public async Task<User> AddAsync(User user, string password, params string[] roles)
        {
            IdentityResult result = await userManager.CreateAsync(user, password);
            if (result.Succeeded == false)
                throw new BusinessRuleException(Localization.ErrorRegisteringUser);

            await AddToRolesAsync(user, roles);
            return user;
        }

        public async Task<User> UpdateAsync(User user, params string[] roles)
        {
            IdentityResult result = await userManager.UpdateAsync(user);
            if (result.Succeeded == false)
                throw new BusinessRuleException(Localization.ErrorRegisteringUser);

            await AddToRolesAsync(user, roles);
            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            IdentityResult result = await userManager.UpdateAsync(user);
            return result.Succeeded == false ? throw new BusinessRuleException(Localization.ErrorRegisteringUser) : user;
        }

        public async Task ResetPasswordAsync(User user, string password)
        {
            string token = await userManager.GeneratePasswordResetTokenAsync(user);

            IdentityResult result = await userManager.ResetPasswordAsync(user, token, password);
            if (result.Succeeded == false)
                throw new BusinessRuleException(Localization.InvalidOperationException);
        }

        public Task ForceDelete(User user)
        {
            return userManager.DeleteAsync(user);
        }

        #region Private Methods

        private static string Normalized(string userName)
        {
            return userName.Trim().ToLower();
        }

        private async Task AddToRolesAsync(User user, params string[] roles)
        {
            IList<string> oldRoles = await userManager.GetRolesAsync(user);
            if (oldRoles.Count > 0)
                await userManager.RemoveFromRolesAsync(user, oldRoles);

            if (roles?.Length > 0)
                await userManager.AddToRolesAsync(user, roles);
        }

        protected static async Task<PageList<User>> GetPagedResultAsync(IQueryable<User> query, int pageNumber = 1, int pageSize = 10)
        {
            var skip = (pageNumber - 1) * pageSize;

            var total = await query.CountAsync();
            var items = await query.Skip(skip).Take(pageSize).ToListAsync();

            return new PageList<User>(items, total, pageNumber, pageSize);
        }

        protected static IQueryable<User> AppendSorting(IQueryable<User> query, PageOption<UserSorting> pagedOptions)
        {
            switch (pagedOptions.SortingBy)
            {
                case UserSorting.FullName:
                    {
                        return pagedOptions.SortingType == PageSortingType.Ascending
                            ? query.OrderBy(q => q.FullName)
                            : query.OrderByDescending(q => q.FullName);
                    }

                case UserSorting.Email:
                    {
                        return pagedOptions.SortingType == PageSortingType.Ascending
                            ? query.OrderBy(q => q.Email)
                            : query.OrderByDescending(q => q.Email);
                    }

                case UserSorting.Id:
                    {
                        return pagedOptions.SortingType == PageSortingType.Ascending
                            ? query.OrderBy(q => q.Id)
                            : query.OrderByDescending(q => q.Id);
                    }

                default:
                    return query.OrderByDescending(q => q.Auditing.CreatedAt);
            }
        }

        #endregion
    }
}
