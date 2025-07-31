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

        public OrderStep? CreateOrderStep(OrderStep orderStep)
        {
            _context.OrderSteps.Add(orderStep);
            _context.SaveChanges();
            return orderStep;
        }

        public Order FindOrderById(int orderId)
        {
            return _context.Orders
                .FirstOrDefault(o => o.Id == orderId) 
                ?? throw new NullReferenceException($"Order with ID {orderId} not found.");
        }

        public List<Order> GetOrdersByDoctorId(Guid doctorId)
        {
            return _context.Orders.Where(x => x.DoctorId == doctorId).ToList();
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
