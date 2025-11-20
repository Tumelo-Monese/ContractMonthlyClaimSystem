using FluentValidation;

namespace ContractMonthlyClaimSystem.Models
{
    public class ClaimValidation : AbstractValidator<Claim>
    {
        public ClaimValidation()
        {
            RuleFor(c => c.Month).InclusiveBetween(1, 12).WithMessage("Please select a valid Month.");

            RuleFor(c => c.Year).InclusiveBetween(2020, 2100).WithMessage("Please enter a valid Year.");

            RuleFor(c => c.TotalHours).InclusiveBetween(0.5m, 500m)
                .WithMessage("Total hours must be between 0.5 and 500.");

            RuleFor(c => c.HourlyRate).InclusiveBetween(1m, 10000m)
                .WithMessage("Hourly rate must be between R1 and R10,000.");

            RuleFor(c => c.TotalAmount).GreaterThan(0)
                .WithMessage("Total amount must be greater than zero.");
        }
    }
}
