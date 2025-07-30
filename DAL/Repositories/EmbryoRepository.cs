using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;

namespace DAL.Repositories
{
    public class EmbryoRepository
    {
        private readonly AppDbContext _context;
        public EmbryoRepository()
        {
            _context = new AppDbContext();
        }
        public int GetEmbryoGainedsByOrderId(int orderId)
        {
            return _context.EmbryoGaineds
                .Where(e => e.OrderId == orderId)
                .Count();
        }
        public int GetEmbryoFrozenByOrderId(int orderId)
        {
            return _context.EmbryoGaineds
                .Where(e => e.OrderId == orderId && e.IsFrozen == true)
                .Count();
        }
    }
}
