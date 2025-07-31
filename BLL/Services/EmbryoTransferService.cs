using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services
{
    public class EmbryoTransferService
    {
        private readonly EmbryoTransferRepository _embryoTransferRepository;
        public EmbryoTransferService()
        {
            _embryoTransferRepository = new EmbryoTransferRepository();
        }
        public List<EmbryoTransfer> GetEmbryoTransfersByOrderId(int orderId)
        {
            return _embryoTransferRepository.GetEmbryoTransfersByOrderId(orderId);
        }
        public EmbryoTransfer AddEmbryoTranfer(EmbryoTransfer embryoTransfer)
        {
            return _embryoTransferRepository.AddEmbryoTransfer(embryoTransfer);
        }
    }
}
