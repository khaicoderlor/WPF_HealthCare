using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services
{
    public class TreatmentStepService
    {
        private readonly TreatmentStepRepository _treatmentStepRepository;
        public TreatmentStepService()
        {
            _treatmentStepRepository = new TreatmentStepRepository();

        }
        public List<ServiceStep> GetStepsByServiceId(int serviceId)
        {
            return _treatmentStepRepository.GetStepbyServiceId(serviceId);
        }
    }
}
