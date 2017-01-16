using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace jobboard.backend.ViewModels
{
    public class SkillDto
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string KeyWords { set; get; }
        public bool IsReg { set; get; }
        public bool IsDirty { set; get; }

        public DateTime UpdatedAt { set; get; }
        
        public int Temperature { set; get; }

        public IEnumerable<ValidationResult> Validate(FluentValidation.ValidationContext validationContext)
        {
            var validator = new SkillDtoValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
