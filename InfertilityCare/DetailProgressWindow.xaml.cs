using DAL.Entities;
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
    /// Interaction logic for DetailProgressWindow.xaml
    /// </summary>
    public partial class DetailProgressWindow : Window
    {
        public List<OrderStep> orderSteps { get; set; }

        private readonly OrderStepRepository orderStepRepository;

        public Order order { get; set; }

        public DetailProgressWindow(Order order)
        {
            InitializeComponent();
            orderStepRepository = new OrderStepRepository();
            this.order = order;
            orderSteps = order.Steps.ToList();
        }

        private void btnMarkCompleted_Click(object sender, RoutedEventArgs e)
        {
            if(dgOrderSteps.SelectedItem is OrderStep selectedStep)
            {
                orderStepRepository.UpdateStatusById(selectedStep.Id, DAL.Enums.StepStatus.Completed);
                MessageBox.Show("Marked completed step successfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnAddEmbryo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddEgg_Click(object sender, RoutedEventArgs e)
        {
            

        }

        private void btnPaid_Click(object sender, RoutedEventArgs e)
        {
            if (dgOrderSteps.SelectedItem is OrderStep selectedStep)
            {
                orderStepRepository.MarkedPaidStep(selectedStep.Id);
                MessageBox.Show("Marked paid step successfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnAddAppointment_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dgOrderSteps_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dgOrderSteps.SelectedItem is OrderStep step)
            {
                txtStep.Text = "Step " + step.ServiceStep.StepOrder;
                txtPaid.Text = step.IsPaid ? "Completed" : "Pending";
                txtContent.Text = step.ServiceStep.Description;
                txtTotalAmount.Text = step.TotalAmount.ToString();
            }
        }
    }
}
