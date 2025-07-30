using BLL.Services;
using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Polices
{
    public class AuthorizationPolicy
    {

        private readonly DoctorRepository _doctorRepository;

        private readonly PatientRepository _patientRepository;  

        public AuthorizationPolicy()
        {
            _doctorRepository = new DoctorRepository();
            _patientRepository = new PatientRepository();
        }

        public bool IsDoctor(ApplicationUser user) 
        {
            return _doctorRepository.FindById(user.Id) != null;
        }   

        public bool IsPatient(ApplicationUser user) 
        {
            return _patientRepository.FindById(user.Id) != null;
        }

        public bool CreatePolicy(ApplicationUser user) 
        {
            return IsDoctor(user);
        }

        public bool ReadPolicy(ApplicationUser user) 
        {
            return IsDoctor(user) || IsPatient(user);
        }

        public bool UpdatePolicy(ApplicationUser user) 
        {
            return IsDoctor(user) || IsPatient(user);
        }
        
        public bool CreateBookingPolicy(ApplicationUser user) 
        {
            return IsPatient(user);
        }

        public bool UpdateProfilePolicy(ApplicationUser user) 
        {
            return IsDoctor(user) || IsPatient(user);
        }

        public bool DeletePolicy(ApplicationUser user) 
        {
            return IsDoctor(user);
        }

        public bool NoPolices(ApplicationUser user) 
        {
            return !IsDoctor(user) && !IsPatient(user);
        }

    }
}
