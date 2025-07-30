using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("Doctor")]
    public class Doctor
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        public DateOnly DateOfBirth { get; set; }

        public string Specialization { get; set; } = string.Empty;

        public int YearsOfExperience { get; set; }

        [Required]
        public Guid ApplicationUserId { get; set; } 

        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
