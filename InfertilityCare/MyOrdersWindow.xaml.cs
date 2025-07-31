using BLL.Services;
using DAL.Entities;
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
    /// Interaction logic for MyOrdersWindow.xaml
    /// </summary>
    public partial class MyOrdersWindow : Window
    {
        private readonly ApplicationUser _authentication;

        private readonly Patient _patient;

        private readonly PatientService _patientService;

        private readonly OrderService _orderService;

        public MyOrdersWindow(ApplicationUser application)
        {
            InitializeComponent();
            _patientService = new PatientService();
            _orderService = new OrderService();
            _authentication = application;
            _patient = _patientService.GetByUserId(_authentication.Id);
            dgOrders.ItemsSource = LoadOrders();
        }

        public List<Order> LoadOrders()
        {
            return _orderService.GetOrderByPatientId(_patient.Id);
        }

        private void btnViewProgress_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            ReportProgressWindow reportProgressWindow = new ReportProgressWindow(int.Parse(txtOrderId.Text));
            reportProgressWindow.ShowDialog();
        }

        private void dgOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dgOrders.SelectedItem is Order selectedOrder)
            {
                txtOrderId.Text = selectedOrder.Id.ToString();
            }
        }
    }
}
