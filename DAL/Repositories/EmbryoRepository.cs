using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Entities;

namespace DAL.Repositories
{
    public class EmbryoRepository
    {
        private readonly AppDbContext _context;
        public EmbryoRepository()
        {
            _context = new AppDbContext();
        }
        public List<EmbryoGained> GetEmbryoGainedsByOrderId(int orderId)
        {
            return _context.EmbryoGaineds
                .Where(e => e.OrderId == orderId)
                .ToList();
        }
        public int GetEmbryoFrozenByOrderId(int orderId)
        {
            return _context.EmbryoGaineds
                .Where(e => e.OrderId == orderId && e.IsFrozen == true)
                .Count();
        }

        public EmbryoGained EmbryoGained(EmbryoGained embryoGained)
        {
            _context.EmbryoGaineds.Add(embryoGained);
            _context.SaveChanges();
            return embryoGained;
        }
    }
}
