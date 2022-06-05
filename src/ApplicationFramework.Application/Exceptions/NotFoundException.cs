namespace ApplicationFramework.Application.Exceptions;

public class NotFoundException : ApplicationException
{
    private const string NotFoundExceptionMessage = "Entity \"{0}\" ({1}) was not found.";

    public NotFoundException(string name, object key) : base(string.Format(NotFoundExceptionMessage, name, key))
    {
    }

    public NotFoundException(string message) : base(message)
    {
    }
}