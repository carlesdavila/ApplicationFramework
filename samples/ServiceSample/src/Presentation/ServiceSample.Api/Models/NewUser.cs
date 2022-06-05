namespace ServiceSample.Api.Models;

public class NewUser
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public UserNewAddress? Address { get; set; }
}