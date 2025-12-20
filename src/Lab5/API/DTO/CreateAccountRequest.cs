namespace Itmo.ObjectOrientedProgramming.Lab5.API.DTO;

public class CreateAccountRequest
{
    public string AccountNumber { get; set; } = string.Empty;

    public string Pin { get; set; } = string.Empty;
}