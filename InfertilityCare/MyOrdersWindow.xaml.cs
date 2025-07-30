using BLL.Services;
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
        private readonly OrderService _orderService;
        private readonly Guid _patientId;

        public MyOrdersWindow(OrderService orderService, Guid patientId)
        {
            InitializeComponent();
            _orderService = orderService;
            _patientId = patientId;

            Loaded += MyOrdersWindow_Loaded;
        }

        private void MyOrdersWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _orderService.GetOrdersForPatientAsync(_patientId)
                .ContinueWith(task =>
                {
                    // Xử lý lỗi
                    if (task.Exception != null)
                    {
                        // Dùng Dispatcher để show MessageBox vì đang ở thread khác
                        Dispatcher.Invoke(() =>
                            MessageBox.Show("Lỗi khi tải danh sách đơn khám: " + task.Exception.InnerException?.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error));
                        return;
                    }

                    var orders = task.Result;

                    // Cập nhật UI trên UI thread
                    Dispatcher.Invoke(() => dgOrders.ItemsSource = orders);
                });
        }


        private void ViewDetails_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var orderId = button?.Tag?.ToString();

            if (!string.IsNullOrEmpty(orderId))
            {
                // Chuyển sang trang chi tiết tiến trình
                var progressWindow = new ReportProgressWindow();
                progressWindow.ShowDialog();
            }
        }
    }
}
