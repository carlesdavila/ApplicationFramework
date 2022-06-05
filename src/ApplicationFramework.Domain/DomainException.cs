namespace ApplicationFramework.Domain;

public class DomainException : Exception
{
    protected DomainException(string businessMessage) : base(businessMessage)
    {
    }
}