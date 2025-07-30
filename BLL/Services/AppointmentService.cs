using BLL.Dtos;
using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AppointmentService
    {
        private readonly AppointmentRepository _appointmentRepository;

        private readonly 

        public AppointmentService()
        {
            _appointmentRepository = new AppointmentRepository();
        }

        public Appointment? AddingAppointmentToOrderStep(CreateAppointmentRequest request)
        {
            if(request.AppointmentDate < DateOnly.FromDateTime(DateTime.Now))
            {
                throw new ArgumentException("Appointment date cannot be in the past.");
            }


        }
    
    }
}
