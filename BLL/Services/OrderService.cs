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

        public async Task<List<OrderViewModel>> GetOrdersForPatientAsync(Guid patientId)
        {
            var orders = await _orderRepository.GetOrdersByPatientIdAsync(patientId);

            var result = orders.Select(o => new OrderViewModel
            {
                Id = o.Id,
                ServiceName = o.Service?.Name ?? "(Không có)",
                DoctorName = o.Doctor?.FullName ?? "(Không rõ)",
                Status = o.Status.ToString(),
                StartDate = o.StartDate,
                EndDate = o.EndDate
            }).ToList();

            return result;
        }
    }
}
