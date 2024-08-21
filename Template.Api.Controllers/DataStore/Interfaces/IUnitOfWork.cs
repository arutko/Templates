namespace Template.Api.Controllers.DataStore.DataStore.Interfaces;

public interface IUnitOfWork
{
    IAccountRepository AccountRepository { get; }
    IPersonRepository PersonRepository { get; }

    Task StartTransactionAsync(CancellationToken cancellationToken);
    Task SubmitTransactionAsync(CancellationToken cancellationToken);
    Task RevertTransactionAsync(CancellationToken cancellationToken);
}
