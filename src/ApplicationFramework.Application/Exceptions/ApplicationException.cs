namespace ApplicationFramework.Application.Exceptions;

public class ApplicationException : Exception
{
    protected ApplicationException(string businessMessage) : base(businessMessage)
    {
    }
}