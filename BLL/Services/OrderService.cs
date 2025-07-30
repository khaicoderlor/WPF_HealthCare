using DAL.Dto;
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

        public OrderService(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
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


    }
}
