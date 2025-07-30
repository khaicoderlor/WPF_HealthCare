using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ApplicationUserService
    {
        private readonly ApplicationUserRepository _applicationUserRepository;

        public ApplicationUserService()
        {
            _applicationUserRepository = new ApplicationUserRepository();
        }

        public ApplicationUser? Authenticate(string email, string password)
        {
            if(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Email and password cannot be null or empty.");
            }

            return _applicationUserRepository.LoginWithCredentials(email, password);
        }

        public ApplicationUser? GetByUserEmail(string email)
        {
            return _applicationUserRepository.FindByEmail(email);
        }

        public ApplicationUser? GetByUserId(Guid id)
        {
            return _applicationUserRepository.FindById(id);

        }
    }

}