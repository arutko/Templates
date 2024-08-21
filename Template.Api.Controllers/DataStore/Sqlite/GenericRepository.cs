using Microsoft.EntityFrameworkCore;
using Template.Api.Controllers.DatabaseContext;
using Template.Api.Controllers.DataStore.DataStore.Interfaces;
using Template.Api.Controllers.Models.Base;

namespace Template.Api.Controllers.DataStore.DataStore.Sqlite;

public class GenericRepository<T> : IGenericRepository<T> where T : Entity
{
    protected readonly AppDbContext _db;

    public GenericRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Guid?> CreateAsync(T entity)
    {
        entity.Active = true;
        await _db.AddAsync(entity);
        await _db.SaveChangesAsync();
        return entity.Id;
    }

    public async Task DeleteAsync(Guid id)
    {
        T? entity = (await _db.Set<T>().FirstOrDefaultAsync(q => q.Id == id))!;
        entity.Active = false;
        await _db.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<T>> ReadAllAsync()
    {
        return await _db.Set<T>()
            .Where(i => i.Active)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<T?> ReadAsync(Guid id)
    {
        return await _db.Set<T>()
            .Where(i => i.Active)
            .AsNoTracking()
            .FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task UpdateAsync(Guid id, T entity)
    {
        entity.Id = id;
        _db.Update(entity);
        await _db.SaveChangesAsync();
    }
}