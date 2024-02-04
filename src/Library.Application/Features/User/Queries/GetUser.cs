using Library.Application.Dtos;
using Library.Application.Exceptions;
using Library.Application.Services.Persistence.Repositories;
using MediatR;

namespace Library.Application.Features.User.Queries
{
    public static class GetUser
    {
        public record Query(Guid Id) : IRequest<UserDetailDto>;
        public sealed class Handler : IRequestHandler<Query, UserDetailDto>
        {
            private readonly IUserRepository _userRepository;
            public Handler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }
            public async Task<UserDetailDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetUserWithAllDependenciesAsync(request.Id, cancellationToken);

                _ = user ?? throw new NotFoundException("Cannot find user by ID");

                return UserDetailDto.CreateFromUser(user);
            }
        }
    }
}
