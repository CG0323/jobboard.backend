using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jobboard.backend.ViewModels
{
    public class SkillDtoValidator : AbstractValidator<SkillDto>
    {
        public SkillDtoValidator()
        {
            RuleFor(skill => skill.Name).NotEmpty().WithMessage("Name cannot be empty");
            RuleFor(skill => skill.KeyWords).NotEmpty().WithMessage("Key Words cannot be empty");
        }
    }
}
