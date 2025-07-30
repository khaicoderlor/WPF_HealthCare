using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Annotations
{
    public class EmbryoGradeValidationAttribute : ValidationAttribute
    {
        private static readonly string[] AllowedGrades = { "AA", "AB", "BB", "BC", "A", "B" };

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string grade && AllowedGrades.Contains(grade))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Grade must be one of: AA, AB, BB, BC, A, B.");
        }
    }

}
