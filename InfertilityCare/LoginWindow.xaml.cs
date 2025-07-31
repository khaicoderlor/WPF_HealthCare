using BLL.Polices;
using BLL.Services;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly ApplicationUserService _userService;

        private readonly AuthorizationPolicy _polices;

        public LoginWindow()
        {
            InitializeComponent();
            _userService = new ApplicationUserService();
            _polices = new AuthorizationPolicy();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var authentication = _userService.Authenticate(txtEmail.Text, txtPassword.Password);
                if (authentication is not null)
                {
                    if (_polices.IsPatient(authentication))
                    {
                        MainWindow home = new MainWindow(authentication);
                        home.Show();
                        this.Close();
                    }
                    else if (_polices.IsDoctor(authentication))
                    {
                        MyPatientsInsideDoctorWindow doctorWindow = new MyPatientsInsideDoctorWindow(authentication);
                        doctorWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show($"You are not authorized to access this application!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show($"Invalid email or password!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
