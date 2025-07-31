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

namespace InfertilityCare
{
    /// <summary>
    /// Interaction logic for AppointmentByStepOrderWindow.xaml
    /// </summary>
    public partial class AppointmentByStepOrderWindow : Window
    {
        public int OrderStepId { get; set; }
        private readonly AppointmentService _appointmentService;
        public AppointmentByStepOrderWindow()
        {
            InitializeComponent();
            LoadAppointmentByStepOrder();
        }
        public void LoadAppointmentByStepOrder()
        {
            dgAppointment.ItemsSource = _appointmentService.GetAppointmentsByOrderStepId(OrderStepId);
        }
    }
}
