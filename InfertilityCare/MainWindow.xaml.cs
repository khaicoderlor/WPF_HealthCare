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
    public partial class MainWindow : Window
    {
        private readonly ApplicationUser _authentication;

        private readonly AuthorizationPolicy _polices;


        public MainWindow(ApplicationUser authentication)
        {
            InitializeComponent();
            _authentication = authentication;
            WelcomeText.Text = $"Welcome, {_authentication.Email}";
            _polices = new AuthorizationPolicy();
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            if (_polices.UpdateProfilePolicy(_authentication))
            {
                ProfileUserWindow profileUserWindow = new ProfileUserWindow(_authentication);
                profileUserWindow.ShowDialog();
                if (profileUserWindow.DialogResult == true)
                {
                    MessageBox.Show("Profile updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            else
            {
                MessageBox.Show("You do not have permission to update your profile.", "Permission Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void MyOrdersButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
