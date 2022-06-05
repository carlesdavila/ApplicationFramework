using ApplicationFramework.Application.Exceptions;
using MediatR;
using ServiceSample.Application.Interfaces;
using ServiceSample.Domain.Entities;

namespace ServiceSample.Application.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
{
    private readonly IServiceSampleDbContext _dbContext;

    public GetUserByIdQueryHandler(IServiceSampleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FindAsync(request.Id);
        return user ?? throw new NotFoundException(nameof(User), request.Id);

    }
}