using Template.Api.Controllers.Models;

namespace Template.Api.Controllers.Services.Contracts;

public interface IAccountService
{
    Task<Guid?> CreateAccount(Account account);
    Task DeleteAccount(Guid accountId);
    Task<Account?> ReadAccount(Guid accountId);
    Task<IEnumerable<Account>> ReadAccounts();
    Task UpdateAccount(Guid accountId, Account account);
    Task<bool> DoesAccountExists(Guid accountId);
}
