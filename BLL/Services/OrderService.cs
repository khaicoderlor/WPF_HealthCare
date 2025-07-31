using DAL.Dto;
using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;

        public OrderService()
        {
            _orderRepository = new OrderRepository();
        }

        public List<OrderViewModel> GetOrdersForPatient(Guid patientId)
        {
            var orders = _orderRepository.GetOrdersByPatientId(patientId);

            return orders.Select(o => new OrderViewModel
            {
                Id = o.Id,
                ServiceName = o.Service?.Name ?? "(Không có)",
                DoctorName = o.Doctor?.FullName ?? "(Không rõ)",
                Status = o.Status.ToString(),
                StartDate = o.StartDate,
                EndDate = o.EndDate
            }).ToList();
        }

        public List<ServiceStepStatusViewModel> GetProgressByOrderId(int orderId)
        {
            var order = _orderRepository.GetOrderWithSteps(orderId);
            if (order == null || order.Service == null) return new();

            var serviceSteps = order.Service.Steps.OrderBy(s => s.StepOrder).ToList();


            var orderSteps = order.Steps.ToList();

            var result = new List<ServiceStepStatusViewModel>();

            foreach (var step in serviceSteps)
            {
                var orderStep = orderSteps.FirstOrDefault(os => os.ServiceStepId == step.Id);

                var status = orderStep != null
                    ? orderStep.Status.ToString()
                    : "InProgress"; // Default nếu chưa làm

                result.Add(new ServiceStepStatusViewModel
                {
                    StepName = step.Name,
                    Description = step.Description,
                    Status = status
                });
            }

            return result;
        }

        public Order GetOrderById(int orderId)
        {
            return _orderRepository.FindOrderById(orderId);
        }

        public List<Order> GetOrderByPatientId(Guid patientId)
        {
            return _orderRepository.GetOrdersByPatientId(patientId);
        }
        public List<Order> GetOrderByDoctorId(Guid doctorId)
        {
            return _orderRepository.GetOrdersByDoctorId(doctorId);
        }
    }
}
