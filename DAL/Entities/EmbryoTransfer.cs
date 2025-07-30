using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("EmbryoTransfer")]
    public class EmbryoTransfer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        [Required]
        public int EmbryoGainedId { get; set; }
        public virtual EmbryoGained EmbryoGained { get; set; }

        [Required]
        public int AppointmentId { get; set; }
        public virtual Appointment Appointment { get; set; }

        public DateTime TransferDate { get; set; }
    }
}
