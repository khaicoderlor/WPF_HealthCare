using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{
    public class CreateAppointmentRequest
    {

        public DateOnly AppointmentDate { get; set; }

        public int OrderStepId { get; set; }

        public string PatientId { get; set; } = string.Empty;

        public int OrderId { get; set; }
        
        public decimal ExrtaFee { get; set; }

    } 
}
