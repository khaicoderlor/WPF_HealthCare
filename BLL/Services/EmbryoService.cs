using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories;

namespace BLL.Services
{
    public class EmbryoService
    {
        private readonly EmbryoRepository _embryoRepository;
        public EmbryoService()
        {
            _embryoRepository = new EmbryoRepository();
        }
        public int GetEmbryoGainedsByOrderId(int orderId)
        {
            return _embryoRepository.GetEmbryoGainedsByOrderId(orderId);
        }
        public int GetEmbryoFrozenByOrderId(int orderId)
        {
            return _embryoRepository.GetEmbryoFrozenByOrderId(orderId);
        }
    }
}
