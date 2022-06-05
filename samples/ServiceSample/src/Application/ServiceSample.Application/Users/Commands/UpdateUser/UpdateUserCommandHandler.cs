using MediatR;
using Microsoft.EntityFrameworkCore;
using ServiceSample.Application.Interfaces;

namespace ServiceSample.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
{
    private readonly IServiceSampleDbContext _dbContext;

    public UpdateUserCommandHandler(IServiceSampleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var userToUpdate = await _dbContext.Users.FirstAsync(x => x.Id == request.Id, cancellationToken);

        userToUpdate.Update(request.Name!, request.Email!);
        
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}