using Cat.Core.Entities;
using System;
using FluentValidation;

namespace Cat.Core.Validation
{
    public class CatValidator : AbstractValidator<Core.Entities.Cat>, IValidator<Core.Entities.Cat>
    {
        public CatValidator()
        {
            RuleFor(c => c.Name).NotEmpty().MaximumLength(50);
            RuleFor(c => c.Age).InclusiveBetween(0, 30);
            RuleFor(c => c.PedigreeNumber).NotEmpty().MaximumLength(20);
            RuleFor(c => c.LastVaccination).LessThanOrEqualTo(DateTime.Today);
            RuleFor(c => c.OwnerId).GreaterThan(0);
            RuleFor(c => c.ClubId).GreaterThan(0);
            RuleFor(c => c.BreedId).GreaterThan(0);
        }
    }
}