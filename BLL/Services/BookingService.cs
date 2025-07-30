using DAL.Context;
using DAL.Entities;
using DAL.Enums;
using System;
using System.Linq;

namespace BLL.Services
{
    public class BookingService
    {
        private readonly AppDbContext _context;

        public BookingService()
        {
            _context = new AppDbContext();
        }

        /// <summary>
        /// Đặt lịch điều trị mới (Booking Order, OrderStep, Appointment)
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="doctorId"></param>
        /// <param name="patientId"></param>
        /// <param name="date"></param>
        /// <param name="slot"></param>
        /// <param name="isFrozen"></param>
        /// <returns>Order đã tạo</returns>
        public Order Book(int serviceId, Guid doctorId, Guid patientId, DateTime date, string slot, bool isFrozen)
        {
            // 1. Tạo Order mới
            var service = _context.Services.FirstOrDefault(s => s.Id == serviceId)
                ?? throw new Exception("Không tìm thấy phác đồ điều trị");

            var doctor = _context.Doctors.FirstOrDefault(d => d.Id == doctorId)
                ?? throw new Exception("Không tìm thấy bác sĩ");

            var patient = _context.Patients.FirstOrDefault(p => p.Id == patientId)
                ?? throw new Exception("Không tìm thấy bệnh nhân");

            var order = new Order
            {
                ServiceId = serviceId,
                Service = service,
                DoctorId = doctorId,
                Doctor = doctor,
                PatientId = patientId,
                Patient = patient,
                Status = OrderStatus.InProgress,
                IsFrozen = isFrozen,
                TotalAmount = 0, // sẽ tính sau nếu cần
                StartDate = date,
                Steps = null // sẽ add bên dưới
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            // 2. Lấy ra bước đầu tiên của phác đồ (IVF/IUI)
            var firstServiceStep = _context.ServiceSteps
                .Where(ss => ss.ServiceId == serviceId)
                .OrderBy(ss => ss.StepOrder)
                .FirstOrDefault();

            if (firstServiceStep == null)
                throw new Exception("Phác đồ điều trị chưa được cấu hình bước");

            // 3. Tạo OrderStep đầu tiên
            var orderStep = new OrderStep
            {
                OrderId = order.Id,
                Order = order,
                ServiceStepId = firstServiceStep.Id,
                ServiceStep = firstServiceStep,
                Status = StepStatus.InProgress,
                TotalAmount = firstServiceStep.Amount,
                StartDate = date,
                Description = firstServiceStep.Description
            };
            _context.OrderSteps.Add(orderStep);
            _context.SaveChanges();

            // 4. Tạo Appointment đầu tiên
            // Parse slot time thành giờ cụ thể (ví dụ "08:00 - 09:00")
            var slotTimes = slot.Split('-');
            var appointmentDateTime = date.Date.Add(TimeSpan.Parse(slotTimes[0].Trim()));

            var appointment = new Appointment
            {
                OrderStepId = orderStep.Id,
                OrderStep = orderStep,
                PatientId = patientId,
                DoctorId = doctorId,
                AppointmentDate = appointmentDateTime,
                Status = AppointmentStatus.Scheduled,
                ExtraFee = 0
            };
            _context.Appointments.Add(appointment);

            _context.SaveChanges();

            return order;
        }
    }
}