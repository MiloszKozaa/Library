using Library.Application.Services.Persistence.Repositories;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Library.Infrastructure.Persistence.Repositories
{
    internal class UserRepository : Repository<User>, IUserRepository
    {
        private readonly LibraryDbContext _context;
        public UserRepository(LibraryDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.Users.AnyAsync(user => user.Email.ToLower() == email.ToLower());
        }

        public async Task<bool> UsernameExistsAsync(string username, CancellationToken cancellationToken)
        {
            return await _context.Users.AnyAsync(user => user.UserName.ToLower().Trim() == username.ToLower().Trim());
        }
    }
}
