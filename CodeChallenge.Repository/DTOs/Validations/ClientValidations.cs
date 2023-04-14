using CodeChallenge.Data.Enums;
using FluentValidation;

namespace CodeChallenge.Data.DTOs.Validations
{
    public class ClientValidations : AbstractValidator<BranchDto>
    {
        public ClientValidations()
        {
            RuleFor(x => x.ClientType)
                .NotNull()
                .WithMessage("ClientType is required");
            RuleFor(x => x.DocNumber)
                .NotNull()
                .WithMessage("Identification number is required"); ;
            RuleFor(x => x.FullName)
                .NotNull()
                .When(x => x.ClientType == ClientType.PersonaNatural)
                .WithMessage("Enter the full name according to the client type entered");
            RuleFor(x => x.CompanyName)
                .NotNull()
                .When(x => x.ClientType == ClientType.PersonaJuridica)
                .WithMessage("Enter the company name according to the client type entered");
            Include(new ContactInformationValidations());
        }
    }
}
