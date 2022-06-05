namespace ApplicationFramework.Application.Exceptions;

public class NotAllowedException : ApplicationException
{
    protected NotAllowedException(string businessMessage) : base(businessMessage)
    {
    }
}