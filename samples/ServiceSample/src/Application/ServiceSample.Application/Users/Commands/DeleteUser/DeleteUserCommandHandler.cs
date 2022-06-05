using MediatR;
using Microsoft.EntityFrameworkCore;
using ServiceSample.Application.Interfaces;

namespace ServiceSample.Application.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
{
    private readonly IServiceSampleDbContext _dbContext;

    public DeleteUserCommandHandler(IServiceSampleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var userToRemove = await _dbContext.Users.FindAsync(request.Id);

        _dbContext.Users.Remove(userToRemove!);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}