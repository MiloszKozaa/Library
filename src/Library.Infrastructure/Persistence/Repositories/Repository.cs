using Library.Application.Services.Persistence;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Infrastructure.Persistence.Repositories
{
    internal class Repository<TModel> : IRepository<TModel> where TModel : Model
    {
        private readonly LibraryDbContext _context;
        public Repository(LibraryDbContext context) 
        {
            _context = context;
        }
        public async Task<TModel> AddAsync(TModel model, CancellationToken cancellationToken = default)
        {
            await _context.Set<TModel>().AddAsync(model, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return model;
        }

        public async Task<IEnumerable<TModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<TModel>().ToListAsync(cancellationToken);
        }

        public async Task<TModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<TModel>().FirstOrDefaultAsync(model => model.Id == id, cancellationToken);
        }

        public async Task<TModel> UpdateAsync(TModel model, CancellationToken cancellationToken = default)
        {
            _context.Set<TModel>().Update(model);
            await _context.SaveChangesAsync(cancellationToken);
            return model;
        }
    }
}
