using Library.Domain.Models;

namespace Library.Application.Services.Persistence;
public interface IRepository<TModel> where TModel : Model
{
    Task<TModel> AddAsync(TModel model, CancellationToken cancellationToken = default);
    Task<IEnumerable<TModel>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<TModel> UpdateAsync(TModel model, CancellationToken cancellationToken= default);
}
