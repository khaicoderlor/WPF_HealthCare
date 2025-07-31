using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Entities;

namespace DAL.Repositories
{
    public class EggRepository
    {
        private readonly AppDbContext _context;
        public EggRepository() 
        {
            _context = new AppDbContext();
        }

        public EggGained AddEggGained(EggGained eggGained)
        {
            _context.EggGaineds.Add(eggGained);
            _context.SaveChanges();
            return eggGained;
        }

        public List<EggGained> GetEggGainedsByOrderId(int orderId)
        {
            return _context.EggGaineds
                .Where(e => e.OrderId == orderId)
                .ToList();
        }
        
    }
}
