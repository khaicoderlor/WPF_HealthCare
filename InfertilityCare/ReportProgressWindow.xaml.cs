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

namespace InfertilityCare
{
    
    public partial class ReportProgressWindow : Window
    {
        private readonly int OrderId;

        private readonly EggService _eggService;
        private readonly EmbryoService _embryoService;
        private readonly EmbryoTransferService _embryoTransferService;

        public ReportProgressWindow(int orderId)
        {
            InitializeComponent();
            _eggService = new EggService();
            _embryoService = new EmbryoService();
            _embryoTransferService = new EmbryoTransferService();
            OrderId = orderId;
            dgEggs.ItemsSource = _eggService.GetEggGainedsByOrderId(OrderId);
            dgEmbryos.ItemsSource = _embryoService.GetEmbryoGainedsByOrderId(OrderId);
            dgEmbryosTransfer.ItemsSource = _embryoTransferService.GetEmbryoTransfersByOrderId(OrderId);
        }

    }
}
