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

        public List<Order> GetOrdersByPatientId(Guid patientId)
        {
            return _context.Orders
                .Where(o => o.PatientId == patientId)
                .ToList();
        }

        public Order GetOrderWithSteps(int orderId)
        {
            return _context.Orders.FirstOrDefault(o => o.Id == orderId);
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
