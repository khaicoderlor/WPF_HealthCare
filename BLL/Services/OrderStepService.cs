using DAL.Constant;
using DAL.Entities;
using DAL.Enums;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderStepService
    {

        private readonly OrderStepRepository _stepRepository;

        private readonly OrderRepository _orderRepository;

        public OrderStepService()
        {
            _stepRepository = new OrderStepRepository();
            _orderRepository = new OrderRepository();
        }

        public OrderStep? CreateOrderStep(OrderStep orderStep)
        {
           return _orderRepository.CreateOrderStep(orderStep);
        }

        public OrderStep? GetById(int id)
        {
            return _stepRepository.FindOrderStepById(id);
        }

        public OrderStep? MarkStatusStepById(int id, string status)
        {
            if (!Enum.TryParse(status, out StepStatus stepStatus))
            {
                throw new ArgumentException($"Invalid status value: {status}");
            }

            var loadedStep = _stepRepository.FindOrderStepById(id)
                             ?? throw new NullReferenceException($"OrderStep with ID {id} not found.");

            var order = loadedStep.Order
                        ?? throw new NullReferenceException($"OrderStep with ID {id} has no associated Order.");

            loadedStep.Status = stepStatus;
            loadedStep.EndDate = DateTime.Now;

            if (stepStatus == StepStatus.Completed)
            {
                var isIVF = order.Service.Name.Equals("IVF", StringComparison.OrdinalIgnoreCase);
                var followUpStep = isIVF ? ApplicationConstant.IVF_FOLLOW_UP : ApplicationConstant.IUI_FOLLOW_UP;

                if (loadedStep.ServiceStep.StepOrder == followUpStep)
                {
                    order.Status = OrderStatus.Completed;
                    order.EndDate = DateTime.Now;
                }
                else if (loadedStep.ServiceStep.StepOrder + 1 < followUpStep)
                {
                    var nextStep = _stepRepository.FindNextStepByOrderStep(loadedStep)
                                   ?? throw new NullReferenceException($"Next step for OrderStep with ID {id} not found.");

                    nextStep.Status = StepStatus.InProgress;
                    nextStep.StartDate = DateTime.Now;
                }
            }

            _stepRepository.SaveChanges();
            return loadedStep;
        }


    }
}
