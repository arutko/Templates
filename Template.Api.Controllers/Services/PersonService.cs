using Template.Api.Controllers.DataStore.DataStore.Interfaces;
using Template.Api.Controllers.Models;
using Template.Api.Controllers.Services.Contracts;

namespace Template.Api.Controllers.Services;

public class PersonService : IPersonService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<PersonService> _logger;

    public PersonService(IUnitOfWork unitOfWork, ILogger<PersonService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task CreatePerson(Person person)
    {
        _logger.LogInformation("Execution of CreatePerson");
        await _unitOfWork.PersonRepository.CreateAsync(person);
    }

    public async Task<Person?> ReadPerson(Guid personId)
    {
        _logger.LogInformation("Execution of ReadPerson");
        return await _unitOfWork.PersonRepository.ReadAsync(personId);
    }

    public async Task<IEnumerable<Person>> ReadPersons()
    {
        _logger.LogInformation("Execution of ReadPersons");
        return await _unitOfWork.PersonRepository.ReadAllAsync();
    }

    public async Task UpdatePerson(Guid personId, Person person)
    {
        _logger.LogInformation("Execution of UpdatePerson");
        await _unitOfWork.PersonRepository.UpdateAsync(personId, person);
    }

    public async Task DeletePerson(Guid personId)
    {
        _logger.LogInformation("Execution of DeletePerson");
        await _unitOfWork.PersonRepository.DeleteAsync(personId);
    }
}
