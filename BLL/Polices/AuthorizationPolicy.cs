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

        public AuthorizationPolicy()
        {
            _applicationUserService = new ApplicationUserService();
        }

        public bool CreatePolicy(ApplicationUser user) 
        {
            
        }

    }
}
