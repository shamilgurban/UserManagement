using FluentValidation;
using Sender.API.Models.Users;

namespace Sender.API.Validators
{
    public class UserCreateValidator : AbstractValidator<UserCreateDto>
    {
        public UserCreateValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Name is required field");
            RuleFor(x => x.Surname).NotNull().NotEmpty().WithMessage("Surname is required field");
            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("Email is required field");
            RuleFor(x => x.PhoneNumber).NotNull().NotEmpty().WithMessage("Phone number is required field");
        }
    }
}
