using DAL.Context;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class OrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrdersByPatientIdAsync(Guid patientId)
        {
            return await _context.Orders
                .Include(o => o.Service)
                .Include(o => o.Doctor)
                .Where(o => o.PatientId == patientId)
                .ToListAsync();
        }
    }
}
