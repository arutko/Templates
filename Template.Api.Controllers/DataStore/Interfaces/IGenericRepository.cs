using Template.Api.Controllers.Models.Base;

namespace Template.Api.Controllers.DataStore.DataStore.Interfaces;

public interface IGenericRepository<T> where T : Entity
{
    Task<Guid?> CreateAsync(T entity);
    Task<IReadOnlyList<T>> ReadAllAsync();
    Task<T?> ReadAsync(Guid id);
    Task UpdateAsync(Guid id, T entity);
    Task DeleteAsync(Guid id);
}