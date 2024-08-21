using Template.Api.Controllers.DataStore.DataStore.Interfaces;
using Template.Api.Controllers.Models;
using Template.Api.Controllers.Services.Contracts;

namespace Template.Api.Controllers.Services;

public class AccountService : IAccountService
{
    private readonly IUnitOfWork _unitOfWork;

    public AccountService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid?> CreateAccount(Account account)
    {
        return await _unitOfWork.AccountRepository.CreateAsync(account);
    }

    public async Task<Account?> ReadAccount(Guid accountId)
    {
        return await _unitOfWork.AccountRepository.ReadAsync(accountId);
    }

    public async Task<IEnumerable<Account>> ReadAccounts()
    {
        return await _unitOfWork.AccountRepository.ReadAllAsync();
    }

    public async Task UpdateAccount(Guid accountId, Account account)
    {
        await _unitOfWork.AccountRepository.UpdateAsync(accountId, account);
    }

    public async Task DeleteAccount(Guid accountId)
    {
        await _unitOfWork.AccountRepository.DeleteAsync(accountId);
    }

    public async Task<bool> DoesAccountExists(Guid accountId)
    {
        return await _unitOfWork.AccountRepository.DoesAccountExists(accountId);
    }
}
