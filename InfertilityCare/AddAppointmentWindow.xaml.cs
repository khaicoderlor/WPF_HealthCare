using BLL.Services;
using DAL.Entities;
using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InfertilityCare
{
    /// <summary>
    /// Interaction logic for AddAppointmentWindow.xaml
    /// </summary>
    public partial class AddAppointmentWindow : Window
    {
        public OrderStep step { get; set; }

        public Order order { get; set; }

        public AppointmentService _appointmentService;

        public AddAppointmentWindow(OrderStep step, Order order)
        {
            InitializeComponent();
            _appointmentService = new AppointmentService();
            this.step = step;
            this.order = order;
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!dpAppointmentDate.SelectedDate.HasValue)
                {
                    MessageBox.Show("Please select a valid appointment date.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                decimal extraFee = 0;
                if (!string.IsNullOrWhiteSpace(txtExtraFee.Text) &&
                    !decimal.TryParse(txtExtraFee.Text, out decimal outxtraFee))
                {
                    MessageBox.Show("Invalid extra fee!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var appointment = new Appointment
                {
                    DoctorId = order.DoctorId,
                    PatientId = order.PatientId,
                    OrderStepId = step.Id,
                    AppointmentDate = DateOnly.FromDateTime(dpAppointmentDate.SelectedDate.Value),
                    ExtraFee = extraFee,
                    Status = AppointmentStatus.Scheduled
                };

                _appointmentService.AddingAppointmentToOrderStep(appointment);

                MessageBox.Show("Added appointment successfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
            

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();   
        }
    }
}
