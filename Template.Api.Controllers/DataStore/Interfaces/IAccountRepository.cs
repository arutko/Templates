using Template.Api.Controllers.Models;

namespace Template.Api.Controllers.DataStore.DataStore.Interfaces;

public interface IAccountRepository : IGenericRepository<Account>
{
    Task<bool> DoesAccountExists(Guid accountId);
}
