using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DAL.Entities;
using BLL.Polices;
using DAL.Repositories;
using BLL.Services;
using DAL.Context;

namespace InfertilityCare
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Static method for testing - bypasses login completely
        public static void ShowTestWindow()
        {
            try
            {
                // Create a dummy user for testing
                var testUser = new ApplicationUser
                {
                    Id = Guid.NewGuid(),
                    Email = "test@test.com",
                    Password = "test"
                };

                var window = new MainWindow(testUser);
                window.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating test window: {ex.Message}", "Error",
                               MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private readonly ApplicationUser _currentUser;
        private readonly Patient? _currentPatient;
        private readonly AuthorizationPolicy? _authPolicy;
        private readonly PatientRepository? _patientRepository;
        private readonly OrderService? _orderService;
        private readonly AppDbContext? _context;

        public MainWindow() : this(new ApplicationUser { Id = Guid.NewGuid(), Email = "test@test.com", Password = "test" })
        {
            // Parameterless constructor for testing or XAML design
        }

        public MainWindow(ApplicationUser user)
        {
            InitializeComponent();
            _currentUser = user ?? new ApplicationUser { Id = Guid.NewGuid(), Email = "fallback@test.com", Password = "test" };

            // COMPLETE BYPASS OF ALL AUTHORIZATION AND DATABASE DEPENDENCIES
            try
            {
                _authPolicy = new AuthorizationPolicy();
                _context = new AppDbContext();
                _patientRepository = new PatientRepository();
                _orderService = new OrderService(new OrderRepository());
            }
            catch (Exception ex)
            {
                // If any service fails to initialize, create dummy instances
                System.Diagnostics.Debug.WriteLine($"Service initialization failed: {ex.Message}");
                _authPolicy = null;
                _context = null;
                _patientRepository = null;
                _orderService = null;
            }

            // COMPLETELY SKIP PATIENT LOOKUP - just set to null
            _currentPatient = null;

            LoadUserInfo();
            LoadServicesData();
        }

        private void LoadUserInfo()
        {
            try
            {
                if (_currentPatient != null)
                {
                    WelcomeText.Text = $"Xin chào, {_currentPatient.FullName}";
                    this.Title = $"InfertilityCare - Xin chào, {_currentPatient.FullName}";
                }
                else
                {
                    WelcomeText.Text = $"Xin chào, {_currentUser.Email}";
                    this.Title = $"InfertilityCare - Xin chào, {_currentUser.Email}";
                }
            }
            catch (Exception ex)
            {
                this.Title = "InfertilityCare - HomePage";
                System.Diagnostics.Debug.WriteLine($"Error loading user info: {ex.Message}");
            }
        }

        private void LoadServicesData()
        {
            try
            {
                // Load hardcoded services data for now since Service table might be empty
                LoadIUIServiceInfo();
                LoadIVFServiceInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi",
                               MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadIUIServiceInfo()
        {
            try
            {
                IUIDescription.Text = "Phương pháp hỗ trợ sinh sản bằng cách đưa tinh trùng đã được xử lý trực tiếp vào buồng tử cung để tăng khả năng thụ thai";

                // Add IUI steps
                IUIStepsPanel.Children.Clear();
                AddServiceStep(IUIStepsPanel, 1, "Kích thích buồng trứng", "Sử dụng thuốc hormone để kích thích buồng trứng sản xuất nhiều trứng hơn", 5000000);
                AddServiceStep(IUIStepsPanel, 2, "Theo dõi nang trứng", "Siêu âm và xét nghiệm hormone để theo dõi sự phát triển của nang trứng", 1000000);
                AddServiceStep(IUIStepsPanel, 3, "Tiêm thuốc kích thích rụng trứng", "Tiêm HCG để kích thích quá trình rụng trứng", 500000);
                AddServiceStep(IUIStepsPanel, 4, "Thụ tinh nhân tạo", "Đưa tinh trùng đã được xử lý vào buồng tử cung", 8000000);

                System.Diagnostics.Debug.WriteLine("IUI Service Info loaded successfully");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading IUI service info: {ex.Message}");
            }
        }

        private void LoadIVFServiceInfo()
        {
            try
            {
                IVFDescription.Text = "Phương pháp hỗ trợ sinh sản bằng cách kết hợp trứng và tinh trùng ngoài cơ thể, sau đó chuyển phôi vào tử cung";

                // Add IVF steps
                IVFStepsPanel.Children.Clear();
                AddServiceStep(IVFStepsPanel, 1, "Kích thích buồng trứng", "Sử dụng thuốc hormone để kích thích buồng trứng sản xuất nhiều trứng", 10000000);
                AddServiceStep(IVFStepsPanel, 2, "Theo dõi nang trứng", "Siêu âm và xét nghiệm hormone để theo dõi sự phát triển của nang trứng", 2000000);
                AddServiceStep(IVFStepsPanel, 3, "Thu trứng", "Thủ thuật lấy trứng từ buồng trứng bằng kim hút", 15000000);
                AddServiceStep(IVFStepsPanel, 4, "Thụ tinh tạo phôi", "Kết hợp trứng và tinh trùng trong phòng thí nghiệm để tạo phôi", 20000000);
                AddServiceStep(IVFStepsPanel, 5, "Chuyển phôi", "Đưa phôi đã phát triển vào buồng tử cung", 10000000);

                System.Diagnostics.Debug.WriteLine("IVF Service Info loaded successfully");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading IVF service info: {ex.Message}");
            }
        }

        private void AddServiceStep(StackPanel panel, int stepOrder, string name, string description, decimal amount)
        {
            var stepBorder = new Border
            {
                Background = new System.Windows.Media.SolidColorBrush(
                    System.Windows.Media.Color.FromRgb(248, 249, 250)),
                BorderBrush = new System.Windows.Media.SolidColorBrush(
                    System.Windows.Media.Color.FromRgb(189, 195, 199)),
                BorderThickness = new Thickness(1),
                Padding = new Thickness(10),
                Margin = new Thickness(0, 3, 0, 0),
                Cursor = System.Windows.Input.Cursors.Hand // Make it clickable
            };

            var stepPanel = new StackPanel();

            var stepTitle = new TextBlock
            {
                Text = $"Bước {stepOrder}: {name}",
                FontWeight = FontWeights.SemiBold,
                FontSize = 13,
                Margin = new Thickness(0, 0, 0, 3)
            };

            stepPanel.Children.Add(stepTitle);

            if (!string.IsNullOrEmpty(description))
            {
                var stepDesc = new TextBlock
                {
                    Text = description,
                    FontSize = 11,
                    Foreground = new System.Windows.Media.SolidColorBrush(
                        System.Windows.Media.Color.FromRgb(127, 140, 141)),
                    TextWrapping = TextWrapping.Wrap,
                    Margin = new Thickness(0, 0, 0, 3)
                };
                stepPanel.Children.Add(stepDesc);
            }

            if (amount > 0)
            {
                var stepAmount = new TextBlock
                {
                    Text = $"Chi phí: {amount:N0} VNĐ",
                    FontSize = 11,
                    FontWeight = FontWeights.SemiBold,
                    Foreground = new System.Windows.Media.SolidColorBrush(
                        System.Windows.Media.Color.FromRgb(231, 76, 60))
                };
                stepPanel.Children.Add(stepAmount);
            }

            // Add click to view details
            var clickHint = new TextBlock
            {
                Text = "👆 Click để xem chi tiết",
                FontSize = 10,
                FontStyle = FontStyles.Italic,
                Foreground = new System.Windows.Media.SolidColorBrush(
                    System.Windows.Media.Color.FromRgb(52, 152, 219)),
                Margin = new Thickness(0, 5, 0, 0)
            };
            stepPanel.Children.Add(clickHint);

            stepBorder.Child = stepPanel;

            // Add click event for step details
            stepBorder.MouseLeftButtonUp += (sender, e) => ShowStepDetails(stepOrder, name, description, amount);

            panel.Children.Add(stepBorder);
        }

        private void ShowStepDetails(int stepOrder, string name, string description, decimal amount)
        {
            string detailMessage = $"CHI TIẾT BƯỚC {stepOrder}\n\n" +
                                 $"Tên: {name}\n\n" +
                                 $"Mô tả: {description}\n\n" +
                                 $"Chi phí: {amount:N0} VNĐ\n\n" +
                                 $"Trạng thái: Chưa thực hiện\n\n" +
                                 $"Ghi chú: Cần hoàn thành bước trước và thanh toán để tiến hành bước này.";

            MessageBox.Show(detailMessage, $"Chi tiết - Bước {stepOrder}",
                          MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BookingButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Open BookingWindow - developed by team Booking (Minh Khải)
                var bookingWindow = new BookingWindow();
                bookingWindow.Show();
                // Don't close MainWindow, let user navigate back
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở BookingWindow: {ex.Message}", "Lỗi",
                               MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Open ProfileUserWindow - developed by team Profile (Thành)
                var profileWindow = new ProfileUserWindow(_currentUser.Id);
                profileWindow.Show();
                // Don't close MainWindow, let user navigate back
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở ProfileUserWindow: {ex.Message}", "Lỗi",
                               MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MyOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Open MyOrdersWindow - developed by team Orders (Thắng)
                if (_orderService != null)
                {
                    // Use current user's ID if no patient record exists
                    var patientId = _currentPatient?.Id ?? _currentUser.Id;
                    var ordersWindow = new MyOrdersWindow(_orderService, patientId);
                    ordersWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Không thể khởi tạo OrderService", "Lỗi",
                                   MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở MyOrdersWindow: {ex.Message}", "Lỗi",
                               MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ProgressButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Open ProgressPatientWindow - developed by team Progress (Thắng)
                if (_orderService != null)
                {
                    // Use dummy orderId = 1 for testing, real implementation will use actual order
                    var progressWindow = new ProgressPatientWindow(1, _orderService);
                    progressWindow.Show();
                    // Don't close MainWindow, let user navigate back
                }
                else
                {
                    MessageBox.Show("Không thể khởi tạo OrderService", "Lỗi",
                                   MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở ProgressPatientWindow: {ex.Message}", "Lỗi",
                               MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận",
                                       MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    // Close database context
                    _context?.Dispose();

                    var loginWindow = new LoginWindow();
                    loginWindow.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi đăng xuất: {ex.Message}", "Lỗi",
                                   MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Clean up resources
            try
            {
                _context?.Dispose();
            }
            catch (Exception ex)
            {
                // Log error but don't prevent window from closing
                System.Diagnostics.Debug.WriteLine($"Error disposing context: {ex.Message}");
            }
        }
    }
}
