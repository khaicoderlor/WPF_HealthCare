using BLL.Services;
using DAL.Context;
using DAL.Entities;
using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace YourNamespace
{
    public partial class BookingWindow : Window
    {

        private readonly TreatmentService _treatmentService;
        private readonly DoctorService _doctorService;
        private readonly PatientService _patientService;
        private readonly AppDbContext _context;
        private readonly OrderStepService _orderStepService;
        private readonly TreatmentStepService _treatmentStepService;
        public ApplicationUser CurrentUser { get; set; }
        public BookingWindow()
        {
            InitializeComponent();
            _treatmentService = new TreatmentService();
            _doctorService = new DoctorService();
            _patientService = new PatientService();
            _context = new AppDbContext();
            _orderStepService = new OrderStepService();
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
            List<OrderStep> orderStep = new List<OrderStep>();
            foreach (var x in _treatmentStepService.GetStepsByServiceId((int)cbService.SelectedValue))
            {
                int a = _context.OrderSteps.Max(x => x.Id) + 1;
                if (x.StepOrder == 1)
                {

                    orderStep.Add(new OrderStep
                    {
                        ServiceStepId = x.Id,
                        Status = StepStatus.InProgress,
                        TotalAmount = x.Amount,
                        StartDate = DateTime.Now,
                        EndDate = null,
                        Appointments = new List<Appointment>()
                    {
                        new Appointment()
                        {
                            OrderStepId = a,
                            DoctorId = (Guid)cbDoctor.SelectedValue,
                            PatientId = _patientService.GetPatientIdbyUserId(CurrentUser.Id),
                            ExtraFee = 0,
                            AppointmentDate = DateOnly.FromDateTime(dpDate.DisplayDate),
                        }
                    }
                    });

                }
                else
                {
                    orderStep.Add(new OrderStep
                    {
                        ServiceStepId = x.Id,
                        Status = StepStatus.InProgress,
                        TotalAmount = x.Amount,
                        StartDate = DateTime.MinValue,
                        EndDate = null,
                        Appointments = new List<Appointment>()
                    });
                } 
                
            }
            Order newOrder = new Order()
            {
                ServiceId = (int)cbService.SelectedValue,
                DoctorId = (Guid)cbDoctor.SelectedValue,
                PatientId = _patientService.GetPatientIdbyUserId(CurrentUser.Id),
                TotalAmount = _context.ServiceSteps
                   .Where(t => t.ServiceId == (int)cbService.SelectedValue)
                   .Sum(x => x.Amount),
                StartDate = DateTime.Now,
                EndDate = null,
                Steps = orderStep
            };
        }
    }
}