using ApplicationFramework.Presentation.Web;
using Microsoft.AspNetCore.Mvc;
using ServiceSample.Api.Models;
using ServiceSample.Application.Users.Commands.CreateUser;
using ServiceSample.Application.Users.Commands.DeleteUser;
using ServiceSample.Application.Users.Commands.UpdateUser;
using ServiceSample.Application.Users.Queries.GetUserById;
using ServiceSample.Application.Users.Queries.GetUsers;

namespace ServiceSample.Api.Controllers;

public class UserController : ApiControllerBase
{
    /// <summary>
    ///     Returns a list of all users
    /// </summary>
    /// <returns> List of users </returns>
    [HttpGet]
    public async Task<ActionResult<List<UserModel>>> GetAll()
    {
        var getUsersQuery = new GetUsersQuery();

        return Ok(await Mediator.Send(getUsersQuery));
    }

    /// <summary>
    ///     Gets a user by id
    /// </summary>
    /// <param name="id"> The id of the user </param>
    /// <returns> The user </returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<UserModel>> Get(Guid id)
    {
        var getUserQuery = new GetUserByIdQuery
        {
            Id = id
        };

        return Ok(await Mediator.Send(getUserQuery));
    }

    /// <summary>
    ///     Creates a new user
    /// </summary>
    /// <param name="newUser"> The user to create </param>
    /// <returns> The created user </returns>
    [HttpPost]
    public async Task<CreatedResult> Create([FromBody] NewUser newUser)
    {
        var command = new CreateUserCommand
        {
            Name = newUser.Name,
            Email = newUser.Email,
            Address = new CreateUserAddress()
            {
                Street = newUser.Address.Street,
                City = newUser.Address.City,
                State = newUser.Address.State,
                Country = newUser.Address.Country,
                ZipCode = newUser.Address.ZipCode
            }
        };

        return Created(string.Empty, await Mediator.Send(command));
    }

    /// <summary>
    ///     Update user
    /// </summary>
    /// <param name="id"> The id of the user </param>
    /// <param name="updateUser"> The user to update </param>
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] UpdateUser updateUser)
    {
        var updateUserCommand = new UpdateUserCommand()
        {
            Id = id,
            Name = updateUser.Name,
            Email = updateUser.Email
        };

        await Mediator.Send(updateUserCommand);
        
        return NoContent();
    }

    /// <summary>
    ///     Deletes an User
    /// </summary>
    /// <param name="id">User identifier</param>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var deleteUserCommand = new DeleteUserCommand
        {
            Id = id
        };

        await Mediator.Send(deleteUserCommand);

        return Ok();
    }
}