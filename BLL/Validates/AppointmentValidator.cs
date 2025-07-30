using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validates
{
    public class AppointmentValidator
    {

        public static ErrorResponse Validate(Appointment appointment)
        {
            var errorResponse = new ErrorResponse();

            if (appointment.AppointmentDate < DateOnly.FromDateTime(DateTime.Now))
            {
                errorResponse.IsError = true;
                errorResponse.Errors.Add("Appointment date cannot be in the past.");
            }

            if(appointment.ExtraFee < 0)
            {
                errorResponse.IsError = true;
                errorResponse.Errors.Add("Extra fee cannot be negative.");
            }

            return errorResponse;
        }
    }
}
