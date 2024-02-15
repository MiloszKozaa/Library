using Library.Application.Exceptions;
using Library.Application.Services.Persistence.Repositories;
using Library.Application.Validators;
using Library.Domain.Models;
using MediatR;
using System.Data;

namespace Library.Application.Features.User.Commands
{
    public static class UpdateUser
    {
        public sealed record UpdateCommand(Guid id, string username, DateTime BirthDate, UserRole role) : IRequest<Unit>;

        public sealed class Handler : IRequestHandler<UpdateCommand, Unit>
        {
            private readonly IUserRepository _userRepository;

            public Handler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<Unit> Handle(UpdateCommand request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetByIdAsync(request.id);

                if(user == null)
                {
                    throw new NotFoundException("Cannot find user by ID");
                }

                if(user.UserName == request.username)
                {
                    throw new BadRequestException("Username do not change");
                }

                List<string> errors = new List<string>();

                if(await _userRepository.UsernameExistsAsync(request.username, cancellationToken))
                {
                    errors.Add("Username already exists");
                }

                if (!Enum.IsDefined(typeof(UserRole), request.role))
                {
                    errors.Add("Role do not exists");
                }

                if (!DateTime.TryParse(request.BirthDate.ToString(), out _))
                {
                    errors.Add("Invalid date");
                }

                if (errors.Count() > 0)
                {
                    throw new ValidatorException(errors.ToArray());
                }

                user.UserName = request.username;

                user.Role = request.role;

                user.BirthDate = request.BirthDate;

                await _userRepository.UpdateAsync(user, cancellationToken);

                return Unit.Value;
            }
        }
    }
}
