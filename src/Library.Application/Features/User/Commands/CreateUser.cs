using FluentValidation;
using Library.Application.Dtos;
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
            var validator = new Validator(_userRepository);
            var result = await validator.ValidateAsync(request, cancellationToken);


            foreach (var failure in result.Errors)
            {
                throw new ValidatorException(failure.ErrorMessage);
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

    public sealed class Validator : AbstractValidator<Command>
    {
        private readonly IUserRepository _userRepository;
        public Validator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(prop => prop.FirstName).NotNull();
            RuleFor(prop => prop.LastName).NotNull();
            RuleFor(prop => prop.Email)
                .NotNull()
                .EmailAddress().WithMessage("Not a valid email address.")
                .MustAsync(async (email, cancellationToken) => !await _userRepository.EmailExistsAsync(email, cancellationToken)).WithMessage("Email already exists.");
            RuleFor(prop => prop.UserName)
                .NotNull()
                .MustAsync(async(username, cancellationToken) => !await _userRepository.UsernameExistsAsync(username, cancellationToken))
                .WithMessage("Username already exists");
            RuleFor(prop => prop.Password)
                .NotNull()
                .MinimumLength(5).WithMessage("Your password length must be at least 5.")
                .MaximumLength(16).WithMessage("Your password length must not exceed 16.");
                //.Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                //.Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                //.Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");
            RuleFor(prop => prop.BirthDate)
                .NotNull()
                .Must(date => DateTime.TryParse(date.ToString(), out _));
            RuleFor(prop => prop.Role)
                .NotNull()
                .Must(role => Enum.IsDefined(typeof(UserRole), role))
                .WithMessage("Role do not exists");
            RuleFor(prop => prop.Address.Country).NotNull();
            RuleFor(prop => prop.Address.State).NotNull();
            RuleFor(prop => prop.Address.City).NotNull();
            RuleFor(prop => prop.Address.PostCode).NotNull();
            RuleFor(prop => prop.Address.StreetName).NotNull();
            RuleFor(prop => prop.Address.StreetNumber).NotNull();
        }

    }
}
