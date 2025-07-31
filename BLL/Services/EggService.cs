using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services
{
    public class EggService
    {
        private readonly EggRepository _eggRepository;
        public EggService()
        {
            _eggRepository = new EggRepository();
        }
        public List<EggGained> GetEggGainedsByOrderId(int orderId)
        {
            return _eggRepository.GetEggGainedsByOrderId(orderId);
        }
        public EggGained AddEggGained(EggGained eggGained)
        {
            return _eggRepository.AddEggGained(eggGained);
        }
    }
}
