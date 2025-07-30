using BLL.Services;
using DAL.Context;
using DAL.Dto;
using DAL.Repositories;
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
    /// Interaction logic for ProgressPatientWindow.xaml
    /// </summary>
    public partial class ProgressPatientWindow : Window
    {
        private readonly OrderService _orderService;
        private readonly int _orderId;

        public ProgressPatientWindow(int orderId, OrderService orderService)
        {
            InitializeComponent();
            _orderId = orderId;
            _orderService = orderService;

            LoadProgress();
        }

        private void LoadProgress()
        {
            try
            {
                var progressSteps = _orderService.GetProgressByOrderId(_orderId);
                dgProgress.ItemsSource = progressSteps;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải tiến độ điều trị: " + ex.Message);
            }
        }
    }


}
