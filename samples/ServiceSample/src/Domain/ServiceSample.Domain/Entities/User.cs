using ServiceSample.Domain.ValueObjects;

namespace ServiceSample.Domain.Entities;

public class User : Entity
{
    public string Name { get; private set; }

    public string Email { get; private set; }

    public Address Address { get; private set; }

    //Constructor needed because EF6 cannot bind parameter constructor to ValueObject 
    private User() { }

    public User(string name, string email, Address address)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(email);
        ArgumentNullException.ThrowIfNull(address);

        Name = name;
        Email = email;
        Address = address;
    }

    public void Update(string name, string email)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Email = email ?? throw new ArgumentNullException(nameof(email));
    }
}
