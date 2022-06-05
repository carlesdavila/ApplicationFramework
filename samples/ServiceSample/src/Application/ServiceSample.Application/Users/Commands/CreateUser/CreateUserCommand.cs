using MediatR;

namespace ServiceSample.Application.Users.Commands.CreateUser;

public class CreateUserAddress
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string ZipCode { get; set; }
}

public class CreateUserCommand : IRequest<Guid>
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public CreateUserAddress Address { get; set; }
}