namespace CleanArchitectureTemplate.Domain.Identity.Users.Interfaces
{
    public interface IUserRepository 
    {
        Task<List<User>> ListAsync();
        Task<User> FindAsync(int id);
        Task<User> GetByUserNameAsync(string userName, bool includeDeleted = false);
        Task<User> AddAsync(User user, string password, params string[] roles);
        Task<User> UpdateAsync(User user);
        Task<User> UpdateAsync(User user, params string[] roles);
        Task ResetPasswordAsync(User user, string password);
        Task ForceDelete(User user);
    }
}
