using DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("Order")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }

        [Required]
        public Guid PatientId { get; set; }
        public virtual Patient Patient { get; set; }

        [Required]
        public Guid DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.InProgress;

        public bool IsFrozen { get; set; } = false;

        public decimal TotalAmount { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public virtual ICollection<OrderStep> Steps { get; set; } = new List<OrderStep>();

    }
}
