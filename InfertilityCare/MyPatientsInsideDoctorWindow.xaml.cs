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
    /// Interaction logic for MyPatientsInsideDoctorWindow.xaml
    /// </summary>
    public partial class MyPatientsInsideDoctorWindow : Window
    {
        private readonly OrderService _orderService;

        private readonly DoctorService _doctorService;

        private ApplicationUser authentication;

        private Doctor doctor;

        public MyPatientsInsideDoctorWindow(ApplicationUser authentication)
        {
            InitializeComponent();
            _orderService = new OrderService();
            _doctorService = new DoctorService();
            this.authentication = authentication;
            this.doctor = _doctorService.GetByUserId(authentication.Id);
            dgOrders.ItemsSource = _orderService.GetOrderByDoctorId(doctor.Id);
        }

        private void dgOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgOrders.SelectedItem is Order selectedOrder)
            {
                txtOrderId.Text = selectedOrder.Id.ToString();
            }
        }

        private void btnViewDetail_Click(object sender, RoutedEventArgs e)
        {
            if (dgOrders.SelectedItem is Order selectedOrder)
            {
                DetailProgressWindow window = new DetailProgressWindow(selectedOrder);
                window.Show();
              
            }
            
        }
    }
}
