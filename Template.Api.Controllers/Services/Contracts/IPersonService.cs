using Template.Api.Controllers.Models;

namespace Template.Api.Controllers.Services.Contracts;

public interface IPersonService
{
    Task CreatePerson(Person person);
    Task DeletePerson(Guid personId);
    Task<Person?> ReadPerson(Guid personId);
    Task<IEnumerable<Person>> ReadPersons();
    Task UpdatePerson(Guid personId, Person person);
}
