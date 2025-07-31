using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Entities;

namespace DAL.Repositories
{
    public class EmbryoTransferRepository
    {
        private readonly AppDbContext _context;
        public EmbryoTransferRepository()
        {
            _context = new AppDbContext();
        }
        public List<EmbryoTransfer> GetEmbryoTransfersByOrderId(int orderId)
        {
            return _context.EmbryoTransfers
                .Where(et => et.OrderId == orderId)
                .ToList();
        } 
    }
}
