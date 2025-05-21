using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cat.Core.Entities;
using FluentValidation;

namespace Cat.Core.Validation
{
    public class OwnerValidator : AbstractValidator<Owner>
    {
        public OwnerValidator()
        {
            RuleFor(o => o.FullName).NotEmpty().MaximumLength(100);
            RuleFor(o => o.PassportNumber).NotEmpty().MaximumLength(50)
                .Matches(@"^[0-9]{4} [0-9]{6}$")
                .WithMessage("Passport number must be in format 'XXXX XXXXXX'");
            RuleFor(o => o.PassportIssueDate).LessThanOrEqualTo(DateTime.Today);
        }
    }
}

