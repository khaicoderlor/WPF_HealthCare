using DAL.Context;
using DAL.Entities;
using DAL.Enums;
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

        public OrderRepository()
        {
            _context = new AppDbContext();
        }

        public async Task<List<Order>> GetOrdersByPatientIdAsync(Guid patientId)
        {
            return await _context.Orders
                .Include(o => o.Service)
                .Include(o => o.Doctor)
                .Where(o => o.PatientId == patientId)
                .ToListAsync();
        }

        public Order? UpdateStatusById(int id, OrderStatus status)
        {
            var order = _context.Orders
                .FirstOrDefault(o => o.Id == id);
            
            if (order == null)
            {
                throw new NullReferenceException($"Order with ID {id} not found.");
            }
            order.Status = status;
            _context.SaveChanges();
            return order;
        }
    }
}
