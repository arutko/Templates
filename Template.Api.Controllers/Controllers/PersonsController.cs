using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template.Api.Controllers.Models;
using Template.Api.Controllers.Services.Contracts;
using Template.Dto.Responses;

namespace Template.Api.Controllers.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class PersonsController : ControllerBase
{
    private readonly IPersonService _personService;
    private readonly IMapper _mapper;

    public PersonsController(IPersonService personService, IMapper mapper)
    {
        _personService = personService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonResponseDto>>> Get()
    {
        var response = await _personService.ReadPersons();
        var toReturn = response.Adapt<IEnumerable<PersonResponseDto>>();
        return Ok(toReturn);
    }

    [HttpGet("{personId}")]
    public async Task<ActionResult<PersonResponseDto?>> Get(Guid personId)
    {
        var response = await _personService.ReadPerson(personId);
        var toReturn = response.Adapt<PersonResponseDto>();
        return Ok(toReturn);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] PersonRequestDto person)
    {
        var toCreate = person.Adapt<Person>();
        await _personService.CreatePerson(toCreate);
        return Created();
    }

    [HttpPut("{personId}")]
    public async Task<ActionResult> Put(Guid personId, [FromBody] PersonRequestDto account)
    {
        var toUpdate = await _personService.ReadPerson(personId);
        var newMapped = _mapper.Map(account, toUpdate!);
        await _personService.UpdatePerson(personId, newMapped);
        return Ok();
    }

    [HttpDelete("{personId}")]
    public async Task<ActionResult> Delete(Guid personId)
    {
        await _personService.DeletePerson(personId);
        return Ok();
    }
}
