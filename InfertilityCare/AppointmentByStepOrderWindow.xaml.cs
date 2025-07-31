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
using BLL.Services;
using DAL.Entities;

namespace InfertilityCare
{
    /// <summary>
    /// Interaction logic for AppointmentByStepOrderWindow.xaml
    /// </summary>
    public partial class AppointmentByStepOrderWindow : Window
    {
        public OrderStep step { get; set; }
        private readonly AppointmentService _appointmentService;
        public AppointmentByStepOrderWindow(OrderStep step)
        {
            InitializeComponent();
            _appointmentService = new AppointmentService();
            this.step = step;
            LoadAppointmentByStepOrder();
        }
        public void LoadAppointmentByStepOrder()
        {
            dgAppointment.ItemsSource = _appointmentService.GetAppointmentsByOrderStepId(step.Id);
        }

        private void btnMakeStatusCompleted_Click(object sender, RoutedEventArgs e)
        {
            if(dgAppointment.SelectedItem is Appointment appointment)
            {
                var updatedAppointment = _appointmentService.MarkStatusAppointment(appointment.Id.ToString(), "Completed");
                if (updatedAppointment != null)
                {
                    MessageBox.Show("Appointment marked as completed successfully.");
                    LoadAppointmentByStepOrder();
                }
                else
                {
                    MessageBox.Show("Failed to mark appointment as completed.");
                }
            }
            else
            {
                MessageBox.Show("Please select an appointment to mark as completed.");
            }
        }
    }
}
