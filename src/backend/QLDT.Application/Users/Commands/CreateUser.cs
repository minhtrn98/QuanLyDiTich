using FluentValidation;
using QLDT.Application.Common.Request;
using QLDT.Domain.UnitOfWork;
using QLDT.Domain.Users;

namespace QLDT.Application.Users.Commands;

public sealed class CreateUser
{
    public sealed record Command(
      string FirstName,
      string LastName,
      string Password,
      string Email
    ) : ICommand<Success>
    {
    }

    public sealed class Handler : ICommandHandler<Command, Success>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Success>> Handle(Command command, CancellationToken cancellationToken)
        {
            User user = new ()
            {
                Id = Guid.NewGuid(),
                Email = command.Email,
                LastName = command.LastName,
                FirstName = command.FirstName,
            };
            Result<Success> result = await _unitOfWork.Users.Create(user, command.Password);
            if (result.IsSuccess)
            {
                await _unitOfWork.SaveChangesAsync();
                return Result.Success;
            }
            return result.Errors;
        }
    }

    public sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
        }
    }
}