using DAL.Context;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class AppointmentRepository
    {

        private readonly AppDbContext _appDbContext;

        public AppointmentRepository()
        {
            _appDbContext = new AppDbContext();
        }

        public Appointment? CreateAppointment(Appointment appointment)
        {
            _appDbContext.Appointments.Add(appointment);
            _appDbContext.SaveChanges();
            return appointment;
        }

        public Appointment? FindAppointmentById(string appointmentId)
        {
            return _appDbContext.Appointments
                .FirstOrDefault(a => a.Id.ToString() == appointmentId);
        }

        public List<Appointment> GetAppointmentsByOrderStepId(int orderStepId)
        {
            return _appDbContext.Appointments.Where(x => x.OrderStepId == orderStepId).ToList();
        }

        public void SaveChanges()
        {
            _appDbContext.SaveChanges();
        }

    }
}
