using DAL.Entities;
using DAL.Repositories;
using DAL.Enums;
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

        private readonly OrderStepRepository _orderStepRepository;

        public AppointmentService()
        {
            _appointmentRepository = new AppointmentRepository();
            _orderStepRepository = new OrderStepRepository();
        }

        public Appointment? AddingAppointmentToOrderStep(Appointment appointment)
        {
            var step = _orderStepRepository.FindOrderStepById(appointment.OrderStepId);
            step.TotalAmount += appointment.ExtraFee;
            _orderStepRepository.SaveChanges();

            _appointmentRepository.CreateAppointment(appointment);
            return appointment;
        }

        public Appointment? MarkStatusAppointment(string appointmentId, string status)
        {
            if(!Enum.TryParse<AppointmentStatus>(status, out var appointmentStatus))
            {
                throw new ArgumentException("Invalid appointment status", nameof(status));
            }

            var appointment = _appointmentRepository.FindAppointmentById(appointmentId);
            appointment.Status = appointmentStatus;
            _appointmentRepository.SaveChanges();
            return appointment;
        }


        public List<Appointment> GetAppointmentsByOrderStepId(int orderStepId)
        {
            return _appointmentRepository.GetAppointmentsByOrderStepId(orderStepId);
        }

        public Appointment? CreateAppointment(Appointment appointment)
        {
            return _appointmentRepository.CreateAppointment(appointment);
        }
    }

}

