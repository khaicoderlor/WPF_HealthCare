using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services
{
    public class TreatmentService
    {
        private readonly TreatmentRepository _treatmentRepository;
        public TreatmentService()
        {
            _treatmentRepository = new TreatmentRepository();
        }
        public List<Service> GetAllServices()
        {
            return _treatmentRepository.GetAllService();
        }
    }
}
