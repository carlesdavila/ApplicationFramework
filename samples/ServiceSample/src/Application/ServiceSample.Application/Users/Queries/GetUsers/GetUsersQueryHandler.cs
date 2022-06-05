using MediatR;
using Microsoft.EntityFrameworkCore;
using ServiceSample.Application.Interfaces;
using ServiceSample.Domain.Entities;

namespace ServiceSample.Application.Users.Queries.GetUsers;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<User>>
{
    private readonly IServiceSampleDbContext _dbContext;

    public GetUsersQueryHandler(IServiceSampleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.ToListAsync(cancellationToken);
    }
}