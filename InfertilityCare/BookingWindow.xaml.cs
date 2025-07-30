using DAL.Context;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace YourNamespace
{
    public partial class BookingWindow : Window
    {
        private List<Service> _services;
        private List<Doctor> _doctors;
        private List<string> _slots = new List<string> { "07:00 - 08:00", "08:00 - 09:00", "09:00 - 10:00", "10:00 - 11:00" };

        public BookingWindow()
        {
            InitializeComponent();
            LoadServices();
            LoadDoctors();
            cbSlot.ItemsSource = _slots;
        }

        private void LoadServices()
        {
            using (var context = new AppDbContext())
            {
                _services = context.Services.ToList();
            }
            cbService.ItemsSource = _services;
        }

        private void LoadDoctors()
        {
            using (var context = new AppDbContext())
            {
                _doctors = context.Doctors.ToList();
            }
            cbDoctor.ItemsSource = _doctors;
        }

        

       

        private void BtnBooking_Click(object sender, RoutedEventArgs e)
        {
            if (cbService.SelectedItem == null || cbDoctor.SelectedItem == null || dpDate.SelectedDate == null || cbSlot.SelectedItem == null)
            {
                MessageBox.Show("Please fill in all required information!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var selectedService = (Service)cbService.SelectedItem;
            var selectedDoctor = (Doctor)cbDoctor.SelectedItem;
            var selectedDate = dpDate.SelectedDate.Value;
            var selectedSlot = cbSlot.SelectedItem.ToString();
            var isFrozen = (selectedService.Name == "IVF") && chkFrozen.IsChecked == true;

            // TODO: Call booking service to save Order, OrderStep, Appointment, etc.

            MessageBox.Show(
                $"Booking successful!\nTreatment: {selectedService.Name}\nDoctor: {selectedDoctor.FullName}\nDate: {selectedDate:d}\nTime slot: {selectedSlot}\nEmbryo freezing: {(isFrozen ? "Yes" : "No")}",
                "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            this.Close();
        }
    }
}