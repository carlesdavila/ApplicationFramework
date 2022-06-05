using ApplicationFramework.Application.Exceptions;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;
using ServiceSample.Application.Interfaces;
using ServiceSample.Domain.Entities;

namespace ServiceSample.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    private readonly IServiceSampleDbContext _dbContext;

    public UpdateUserCommandValidator(IServiceSampleDbContext dbContext)
    {
        _dbContext = dbContext;
        
        RuleFor(x => x.Id).MustAsync(Exist);
    }

    private async Task<bool> Exist(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.AnyAsync(x => x.Id == id, cancellationToken)
            ? true
            : throw new NotFoundException(nameof(User), id);
    }
}