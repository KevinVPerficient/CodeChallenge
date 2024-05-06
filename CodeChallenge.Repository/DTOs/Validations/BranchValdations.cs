using FluentValidation;
namespace CodeChallenge.Data.DTOs.Validations
{
    public class BranchValdations : AbstractValidator<BranchDto>
    {
        public BranchValdations() 
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Code is required")
                .Length(3, 5).WithMessage("Enter a value between 3 and 5 characters"); 
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(5, 100).WithMessage("Enter a value between 5 and 100 characters");
            RuleFor(x => x.Credit)
                .NotEmpty().WithMessage("Credit is required")
                .GreaterThan(0).WithMessage("Enter a value greater than 0");
            Include(new ContactInformationValidations());
        }
    }
}
