using MediatR;
using ServiceSample.Application.Interfaces;
using ServiceSample.Domain.Entities;
using ServiceSample.Domain.ValueObjects;

namespace ServiceSample.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IServiceSampleDbContext _dbContext;

    public CreateUserCommandHandler(IServiceSampleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var newAddress = new Address(request.Address.Street, request.Address.City, request.Address.State, request.Address.Country, request.Address.ZipCode);
        var newUser = new User(request.Name!, request.Email!, newAddress);

        _dbContext.Users.Add(newUser);

        _ = await _dbContext.SaveChangesAsync(cancellationToken);

        return newUser.Id;
    }
}