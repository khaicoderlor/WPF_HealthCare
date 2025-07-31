using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services
{
    public class DoctorService
    {
        private readonly DoctorRepository _doctorRepository;
        public DoctorService()
        {
            _doctorRepository = new DoctorRepository();
        }
        public List<Doctor> GetAllDoctors()
        {
            return _doctorRepository.GetAllDoctors();
        }

        public Doctor? GetByUserId(Guid userId)
        {
            return _doctorRepository.FindByApplicationUser(userId);
        }
    }
}
