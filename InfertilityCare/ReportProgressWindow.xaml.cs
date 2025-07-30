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
        public int orderId { get; set; }
        private readonly EggService _eggService;
        private readonly EmbryoService _embryoService;
        private readonly EmbryoTransferService _embryoTransferService;
        public ReportProgressWindow()
        {
            InitializeComponent();
            _eggService = new EggService();
            _embryoService = new EmbryoService();
            _embryoTransferService = new EmbryoTransferService();
            LoadData();
        }

        private void LoadData()
        {
            txtNumberEmbryo.Text = _embryoService.GetEmbryoGainedsByOrderId(orderId).ToString();
            txtNumberFrozenEmbryo.Text = _embryoService.GetEmbryoGainedsByOrderId(orderId).ToString();
            txtNumberOfEgg.Text = _eggService.GetEggGainedsByOrderId(orderId).ToString();
            txtNumberTransferEmbryo.Text = _embryoTransferService.GetEmbryoTransfersByOrderId(orderId).ToString();
        }

    }
}
