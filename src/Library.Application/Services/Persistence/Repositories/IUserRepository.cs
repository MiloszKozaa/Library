using Library.Domain.Models;

namespace Library.Application.Services.Persistence.Repositories;
public interface IUserRepository : IRepository<User>
{
    Task<bool> UsernameExistsAsync(string username, CancellationToken cancellationToken = default);
    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default);
    Task<User> GetUserWithAllDependenciesAsync(Guid id, CancellationToken cancellationToken = default);
}
