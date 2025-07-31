using DAL.Context;
using DAL.Entities;
using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class OrderStepRepository
    {

        private readonly AppDbContext _context;

        public OrderStepRepository()
        {
            _context = new AppDbContext();
        }

        public OrderStep? FindOrderStepById(int id)
        {
            return _context.OrderSteps
                    .FirstOrDefault(orderStep => orderStep.Id == id);
        }

        public OrderStep? UpdateStatusById(int id, StepStatus status)
        {
            var orderStep = FindOrderStepById(id);
            if (orderStep == null)
            {
                throw new NullReferenceException($"OrderStep with ID {id} not found.");
            }
            orderStep.Status = status;
            _context.SaveChanges();
            return orderStep;
        }

        public OrderStep? MarkedPaidStep(int id)
        {
            var orderStep = FindOrderStepById(id);
            if (orderStep == null)
            {
                throw new NullReferenceException($"OrderStep with ID {id} not found.");
            }
            orderStep.IsPaid = true;
            _context.SaveChanges();
            return orderStep;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        } 
        
        public OrderStep? FindNextStepByOrderStep(OrderStep step)
        {
            return _context.OrderSteps.Where(x => x.OrderId == step.OrderId && x.ServiceStep.StepOrder == step.ServiceStep.StepOrder + 1).FirstOrDefault();
        }

    }
}
