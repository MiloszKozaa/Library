using Library.Application.Exceptions;
using Library.Application.Services.Persistence.Repositories;
using Library.Domain.Models;
using MediatR;

namespace Library.Application.Features.User.Commands
{
    public static class UpdateUser
    {
        public sealed record Command(Guid id, string username, DateTime BirthDate, UserRole role) : IRequest<object>;

        public sealed class Handler : IRequestHandler<Command, object>
        {
            private readonly IUserRepository _userRepository;

            public Handler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<object> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetByIdAsync(request.id);

                if(user == null)
                {
                    throw new NotFoundException("Cannot find user by ID");
                }

                user.UserName = request.username;

                user.Role = request.role;

                user.BirthDate = request.BirthDate;



                return new() { };
            }
        }
    }
}
