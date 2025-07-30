using DAL.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class EmbryoGained
    {
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        [Required]
        public int EggGainedId { get; set; }
        public virtual EggGained EggGained { get; set; }

        [Required]
        [EmbryoGradeValidation]
        public string Grade { get; set; }

        public bool Status { get; set; } = true;

        public bool IsFrozen { get; set; } = false;

        public DateTime DateGained { get; set; } = DateTime.Now;
    }
}
