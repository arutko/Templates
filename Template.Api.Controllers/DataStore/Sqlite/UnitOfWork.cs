using Template.Api.Controllers.DatabaseContext;
using Template.Api.Controllers.DataStore.DataStore.Interfaces;

namespace Template.Api.Controllers.DataStore.DataStore.Sqlite;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _appDbContext;

    public IAccountRepository AccountRepository { get; private set; }
    public IPersonRepository PersonRepository { get; private set; }

    public UnitOfWork(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;

        AccountRepository = new AccountRepository(_appDbContext);
        PersonRepository = new PersonRepository(_appDbContext);
    }

    public async Task RevertTransactionAsync(CancellationToken cancellationToken)
    {
        await _appDbContext.Database.RollbackTransactionAsync(cancellationToken);
    }

    public async Task StartTransactionAsync(CancellationToken cancellationToken)
    {
        await _appDbContext.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task SubmitTransactionAsync(CancellationToken cancellationToken)
    {
        await _appDbContext.Database.CommitTransactionAsync(cancellationToken);
    }
}
