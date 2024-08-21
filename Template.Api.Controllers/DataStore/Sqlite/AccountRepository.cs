using Microsoft.EntityFrameworkCore;
using Template.Api.Controllers.DatabaseContext;
using Template.Api.Controllers.DataStore.DataStore.Interfaces;
using Template.Api.Controllers.Models;

namespace Template.Api.Controllers.DataStore.DataStore.Sqlite;

public class AccountRepository : GenericRepository<Account>, IAccountRepository
{
    public AccountRepository(AppDbContext db) : base(db)
    {
    }

    public async Task<bool> DoesAccountExists(Guid accountId)
        => await _db.Accounts.AnyAsync(a => a.Id == accountId && a.Active);
}
