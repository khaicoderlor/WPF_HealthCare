using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;

namespace DAL.Repositories
{
    public class EmbryoTransferRepository
    {
        private readonly AppDbContext _context;
        public EmbryoTransferRepository()
        {
            _context = new AppDbContext();
        }
        public int GetEmbryoTransfersByOrderId(int orderId)
        {
            return _context.EmbryoTransfers
                .Where(et => et.OrderId == orderId)
                .Count();
        } 
    }
}
