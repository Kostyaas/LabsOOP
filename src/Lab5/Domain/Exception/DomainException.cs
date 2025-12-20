namespace Itmo.ObjectOrientedProgramming.Lab5.Domain.Exception;

public abstract class DomainException : System.Exception
{
    protected DomainException() { }

    protected DomainException(string message) : base(message) { }

    protected DomainException(string message, System.Exception innerException) : base(message, innerException) { }
}