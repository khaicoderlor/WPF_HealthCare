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

        private readonly Order order;
        public MyPatientsInsideDoctorWindow(Order order)
        {
            InitializeComponent();
            _orderService = new OrderService();
            this.order = order;
        }

        private void dgOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgOrders.SelectedItem is DAL.Entities.Order selectedOrder)
            {
                txtOrderId.Text = selectedOrder.Id.ToString();
            }
        }

        private void btnViewDetail_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
