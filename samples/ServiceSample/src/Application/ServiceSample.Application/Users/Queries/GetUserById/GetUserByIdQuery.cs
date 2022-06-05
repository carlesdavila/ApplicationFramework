using MediatR;
using ServiceSample.Domain.Entities;

namespace ServiceSample.Application.Users.Queries.GetUserById;

public class GetUserByIdQuery : IRequest<User>
{
    public Guid Id { get; set; }
}