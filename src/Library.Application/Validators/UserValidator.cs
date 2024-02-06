using FluentValidation;
using Library.Application.Services.Persistence.Repositories;
using static Library.Application.Features.User.Commands.CreateUser;

namespace Library.Application.Validators
{
    public sealed class UserValidator : AbstractValidator<Command>
    {
        private readonly IUserRepository _userRepository;
        public UserValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(prop => prop.FirstName).NotNull();
            RuleFor(prop => prop.LastName).NotNull();
            RuleFor(prop => prop.Email)
                .NotNull()
                .EmailAddress()
                .WithMessage("Not a valid email address.")
                .MustAsync(async (email, cancellationToken) => !await _userRepository.EmailExistsAsync(email, cancellationToken))
                .WithMessage("Email already exists.");
            RuleFor(prop => prop.UserName)
                .NotNull()
                .MustAsync(async (username, cancellationToken) => !await _userRepository.UsernameExistsAsync(username, cancellationToken))
                .WithMessage("Username already exists");
            RuleFor(prop => prop.Password)
                .NotNull()
                .MinimumLength(5)
                .WithMessage("Your password length must be at least 5.")
                .MaximumLength(16)
                .WithMessage("Your password length must not exceed 16.");
            RuleFor(prop => prop.BirthDate)
                .NotNull()
                .Must(date => DateTime.TryParse(date.ToString(), out _));
            RuleFor(prop => prop.Role)
                .NotNull()
                .Must(role => Enum.IsDefined(typeof(Domain.Models.UserRole), role))
                .WithMessage("Role do not exists");
            RuleFor(prop => prop.Address)
                .NotNull()
                .Must(address => Validation.IsValidType(address))
                .WithMessage("Address prop must be type of Address")
                .SetValidator(new AddressValidator())
                .When(address => Validation.IsValidType(address));
        }

    }
}
