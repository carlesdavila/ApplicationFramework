using MediatR;
using ServiceSample.Domain.Entities;

namespace ServiceSample.Application.Users.Queries.GetUsers;
public class GetUsersQuery : IRequest<List<User>>
{

}