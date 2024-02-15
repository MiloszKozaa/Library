using FluentValidation;
using static Library.Application.Features.User.Commands.CreateUser;

namespace Library.Application.Validators.User
{
    public sealed class AddressValidator : AbstractValidator<Address?>
    {
        public AddressValidator()
        {

            RuleFor(prop => prop.Country).NotNull();
            RuleFor(prop => prop.State).NotNull();
            RuleFor(prop => prop.City).NotNull();
            RuleFor(prop => prop.StreetName).NotNull();
            RuleFor(prop => prop.StreetNumber).NotNull();


        }
    }
}
