using Library.Application.Dtos;
using Library.Application.Models;
using Library.Application.Services.Persistence.Repositories;
using MediatR;

namespace Library.Application.Features.User.Queries
{
    public static class GetUsers
    {
        public record Query() : IRequest<ListModel<UserListDto>>;
        public sealed class Handler : IRequestHandler<Query, ListModel<UserListDto>>
        {
            private readonly IUserRepository _userRepository;
            public Handler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }
            public async Task<ListModel<UserListDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var users = await _userRepository.GetAllAsync(cancellationToken);

                return ListModel<UserListDto>.Create(users.Select(user => UserListDto.CreateFromUser(user)));
            }
        }
    }
}
