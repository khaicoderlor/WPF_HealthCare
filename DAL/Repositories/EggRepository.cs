using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;

namespace DAL.Repositories
{
    public class EggRepository
    {
        private readonly AppDbContext _context;
        public EggRepository() 
        {
            _context = new AppDbContext();
        }
        public int GetEggGainedsByOrderId(int orderId)
        {
            return _context.EggGaineds
                .Where(e => e.OrderId == orderId)
                .Count();
        }
        
    }
}
