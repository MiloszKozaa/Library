using Library.Application.Exceptions;
using Library.Application.Services.Persistence.Repositories;
using Library.Domain.Models;
using MediatR;

namespace Library.Application.Features.User.Commands;
public static class CreateUser
{
    public sealed record Address(string Country, string State, string City, string PostCode, string StreetName, string StreetNumber, string? FlatNumber);
    public sealed record Command(string FirstName, string LastName, string Email, string UserName, string Password, DateTime BirthDate, UserRole Role, Address Address) : IRequest<object>;

    public sealed class Handler : IRequestHandler<Command, object>
    {
        private readonly IUserRepository _userRepository;
        public Handler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<object> Handle(Command request, CancellationToken cancellationToken)
        {
            var validator = new UserValidator(_userRepository);
            var result = await validator.ValidateAsync(request, cancellationToken);


            foreach (var failure in result.Errors)
            {
                throw new BadRequestException(failure.ErrorMessage);
            }

            var user = new Domain.Models.User
            {
                FirstName = request.FirstName, 
                LastName = request.LastName, 
                Email = request.Email, 
                UserName = request.UserName, 
                Password = request.Password, 
                BirthDate = request.BirthDate, 
                Role = request.Role,
                Address = new Domain.Models.Address
                {
                    Country = request.Address.Country,
                    City = request.Address.City,
                    State = request.Address.State,
                    PostCode = request.Address.PostCode,
                    StreetName = request.Address.StreetName,
                    StreetNumber = request.Address.StreetNumber,
                    FlatNumber = request.Address.FlatNumber
                }
            };

            await _userRepository.AddAsync(user, cancellationToken);

            return new() { };
        }
    }
}
