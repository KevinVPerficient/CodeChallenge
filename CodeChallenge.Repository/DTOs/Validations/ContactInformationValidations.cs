using FluentValidation;

namespace CodeChallenge.Data.DTOs.Validations
{
    public class ContactInformationValidations : AbstractValidator<ContactInformationDto>
    {
        public ContactInformationValidations()
        {
            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage("Address is required");
            RuleFor(x => x.City)
                .NotEmpty()
                .WithMessage("City is required");
            RuleFor(x => x.Country)
                .NotEmpty()
                .WithMessage("Address is required"); 
            RuleFor(x => x.PhoneNumber)
                .Length(7, 12)
                .NotNull()
                .When(x => x.CellPhoneNumber == null)
                .WithMessage("Enter at least one contact number");
            RuleFor(x => x.CellPhoneNumber)
                .Length(10, 15)
                .NotNull()
                .When(x => x.PhoneNumber == null)
                .WithMessage("Enter at least one contact number");
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Enter a valid email");
        }
    }
}
