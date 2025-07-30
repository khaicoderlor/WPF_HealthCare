using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Annotations
{
    public class EggGradeValidationAttribute : ValidationAttribute
    {
        private static readonly string[] AllowedGrades = new[] { "M2", "M1", "GV" };

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string grade && AllowedGrades.Contains(grade))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Grade must be one of: M2, M1, GV.");
        }
    }
}
