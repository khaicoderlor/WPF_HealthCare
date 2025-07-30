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
using DAL.Repositories;

namespace InfertilityCare
{
    /// <summary>
    /// Interaction logic for ProfileUserWindow.xaml
    /// </summary>
    public partial class ProfileUserWindow : Window
    {
        private readonly PatientService _patientService;
        private Patient? _currentPatient;

        public ProfileUserWindow(Guid applicationUserId)
        {
            InitializeComponent();
            _patientService = new PatientService();
            LoadPatient(applicationUserId);
        }

        private void LoadPatient(Guid userId)
        {
            _currentPatient = _patientService.GetByUserId(userId);
            if (_currentPatient != null)
            {
                txtFullName.Text = _currentPatient.FullName;
                dpDOB.SelectedDate = _currentPatient.DateOfBirth.ToDateTime(TimeOnly.MinValue);
                txtPartnerFullName.Text = _currentPatient.PartnerFullName;
                txtPartnerEmail.Text = _currentPatient.PartnerEmail;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPatient == null) return;

            _currentPatient.FullName = txtFullName.Text.Trim();
            _currentPatient.PartnerFullName = txtPartnerFullName.Text.Trim();
            _currentPatient.PartnerEmail = txtPartnerEmail.Text.Trim();

            if (dpDOB.SelectedDate.HasValue)
                _currentPatient.DateOfBirth = DateOnly.FromDateTime(dpDOB.SelectedDate.Value);

            try
            {
                _patientService.Update(_currentPatient);
                MessageBox.Show("Update successfull!", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Exception: {ex.Message}", "Exceptions", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
