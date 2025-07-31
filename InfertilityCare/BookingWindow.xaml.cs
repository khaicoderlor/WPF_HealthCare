using BLL.Services;
using DAL.Context;
using DAL.Entities;
using DAL.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace InfertilityCare
{
    public partial class BookingWindow : Window
    {

        private readonly TreatmentService _treatmentService;
        private readonly DoctorService _doctorService;
        private readonly PatientService _patientService;
        private readonly AppDbContext _context;
        private readonly OrderStepService _orderStepService;
        private readonly TreatmentStepService _treatmentStepService;
        private readonly AppointmentService _appointmentService = new AppointmentService();
        public ApplicationUser CurrentUser { get; set; }
        public BookingWindow(ApplicationUser CurrentUser)
        {
            InitializeComponent();
            _treatmentService = new TreatmentService();
            _doctorService = new DoctorService();
            _patientService = new PatientService();
            _context = new AppDbContext();
            _orderStepService = new OrderStepService();
            this.CurrentUser = CurrentUser;
            _treatmentStepService = new TreatmentStepService();

            LoadService();
            LoadDoctor();
        }


        private void LoadService()
        {
            cbService.ItemsSource = _treatmentService.GetAllServices();
            cbService.DisplayMemberPath = "Name";
            cbService.SelectedValuePath = "Id";
        }
        private void LoadDoctor()
        {
            cbDoctor.ItemsSource = _doctorService.GetAllDoctors();
            cbDoctor.DisplayMemberPath = "FullName";
            cbDoctor.SelectedValuePath = "Id";
        }
        private void BtnBooking_Click(object sender, RoutedEventArgs e)
        {
            // Lấy dữ liệu cần thiết
            var serviceId = (int)cbService.SelectedValue;
            var doctorId = (Guid)cbDoctor.SelectedValue;
            var patientId = _patientService.GetPatientIdbyUserId(CurrentUser.Id);
            var serviceSteps = _treatmentStepService.GetStepsByServiceId(serviceId);
            var appointmentDate = DateOnly.FromDateTime(dpDate.DisplayDate);

            // Tạo Order
            var newOrder = new Order()
            {
                ServiceId = serviceId,
                DoctorId = doctorId,
                PatientId = patientId,
                TotalAmount = _context.ServiceSteps
                    .Where(s => s.ServiceId == serviceId)
                    .Sum(s => s.Amount),
                StartDate = DateTime.Now,
                EndDate = null
            };
            _context.Orders.Add(newOrder);
            _context.SaveChanges(); // để newOrder.Id có giá trị

            // Duyệt từng ServiceStep
            foreach (var step in serviceSteps)
            {
                Console.WriteLine(step.ServiceId);
                // Tạo OrderStep
                var newStep = new OrderStep()
                {
                    OrderId = newOrder.Id,
                    ServiceStepId = step.Id,
                    TotalAmount = step.Amount,
                    StartDate = step.StepOrder == 1 ? DateTime.Now : DateTime.MinValue,
                    EndDate = null,
                    Status = step.StepOrder == 1 ? StepStatus.InProgress : StepStatus.Scheduled
                };

                _context.OrderSteps.Add(newStep);
                _context.SaveChanges(); // để newStep.Id có giá trị khi tạo Appointment

                // Tạo Appointment gắn với OrderStep
                var appointment = new Appointment()
                {
                    OrderStepId = newStep.Id,
                    DoctorId = doctorId,
                    PatientId = patientId,
                    ExtraFee = 0,
                    AppointmentDate = appointmentDate,
                    Status = AppointmentStatus.Scheduled
                };

                _context.Appointments.Add(appointment);
                _context.SaveChanges(); // nếu bạn muốn tạo lần lượt

            }
            MessageBox.Show("Booking successful!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            DialogResult = true;

        }
    }
}

