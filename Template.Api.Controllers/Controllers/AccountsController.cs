using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template.Api.Controllers.Services.Contracts;
using Template.Dto.Responses;

namespace Template.Api.Controllers.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;

    public AccountsController(IAccountService accountService, IMapper mapper)
    {
        _accountService = accountService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AccountResponseDto>>> Get()
    {
        var response = await _accountService.ReadAccounts();
        var toReturn = response.Adapt<IEnumerable<AccountResponseDto>>();
        return Ok(toReturn);
    }

    [HttpGet("{accountId}")]
    public async Task<ActionResult<AccountResponseDto?>> Get(Guid accountId)
    {
        var response = await _accountService.ReadAccount(accountId);
        var toReturn = response.Adapt<AccountResponseDto>();
        return Ok(toReturn);
    }

    [HttpPut("{accountId}")]
    public async Task<ActionResult> Put(Guid accountId, [FromBody] AccountRequestDto account)
    {
        var toUpdate = await _accountService.ReadAccount(accountId);
        var newMapped = _mapper.Map(account, toUpdate!);
        await _accountService.UpdateAccount(accountId, newMapped);
        return Ok();
    }

    [HttpDelete("{accountId}")]
    public async Task<ActionResult> Delete(Guid accountId)
    {
        await _accountService.DeleteAccount(accountId);
        return Ok();
    }
}
