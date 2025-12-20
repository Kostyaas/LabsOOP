using Itmo.ObjectOrientedProgramming.Lab5.API.DTO;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Itmo.ObjectOrientedProgramming.Lab5.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ATMController : ControllerBase
{
    private readonly IATMService _atmService;

    public ATMController(IATMService atmService)
    {
        _atmService = atmService;
    }

    [HttpPost("session/user")]
    public async Task<IActionResult> CreateUserSession([FromBody] CreateUserSessionRequest request)
    {
        try
        {
            Guid sessionId = await _atmService.CreateUserSessionAsync(
                request.AccountNumber,
                request.Pin);

            return Ok(new { SessionId = sessionId });
        }
        catch (UnauthorizedAccessException ex)
        {
            return StatusCode(401, new { Error = ex.Message });
        }
    }

    [HttpPost("session/admin")]
    public async Task<IActionResult> CreateAdminSession([FromBody] CreateAdminSessionRequest request)
    {
        try
        {
            Guid sessionId = await _atmService.CreateAdminSessionAsync(request.AdminPassword);
            return Ok(new { SessionId = sessionId });
        }
        catch (UnauthorizedAccessException ex)
        {
            return StatusCode(401, new { Error = ex.Message });
        }
    }

    [HttpPost("accounts")]
    public async Task<IActionResult> CreateAccount(
        [FromBody] CreateAccountRequest request,
        [FromHeader] Guid sessionId)
    {
        try
        {
            Guid accountId = await _atmService.CreateAccountAsync(
                request.AccountNumber,
                request.Pin,
                sessionId);

            return Ok(new { AccountId = accountId });
        }
        catch (UnauthorizedAccessException ex)
        {
            return StatusCode(401, new { Error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpGet("accounts/{accountId}/balance")]
    public async Task<IActionResult> GetBalance(Guid accountId, [FromHeader] Guid sessionId)
    {
        try
        {
            decimal balance = await _atmService.GetBalanceAsync(accountId, sessionId);
            return Ok(new { Balance = balance });
        }
        catch (UnauthorizedAccessException ex)
        {
            return StatusCode(401, new { Error = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpPost("accounts/{accountId}/withdraw")]
    public async Task<IActionResult> Withdraw(
        Guid accountId,
        [FromBody] AmountRequest request,
        [FromHeader] Guid sessionId)
    {
        try
        {
            decimal balance = await _atmService.WithdrawAsync(accountId, request.Amount, sessionId);
            return Ok(new { Balance = balance });
        }
        catch (UnauthorizedAccessException ex)
        {
            return StatusCode(401, new { Error = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpPost("accounts/{accountId}/deposit")]
    public async Task<IActionResult> Deposit(
        Guid accountId,
        [FromBody] AmountRequest request,
        [FromHeader] Guid sessionId)
    {
        try
        {
            decimal balance = await _atmService.DepositAsync(accountId, request.Amount, sessionId);
            return Ok(new { Balance = balance });
        }
        catch (UnauthorizedAccessException ex)
        {
            return StatusCode(401, new { Error = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpGet("accounts/{accountId}/transactions")]
    public async Task<IActionResult> GetTransactions(Guid accountId, [FromHeader] Guid sessionId)
    {
        try
        {
            IEnumerable<Domain.Entities.Transaction> transactions = await _atmService.GetTransactionHistoryAsync(accountId, sessionId);
            return Ok(transactions);
        }
        catch (UnauthorizedAccessException ex)
        {
            return StatusCode(401, new { Error = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}