using DAL.Context;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ApplicationUserRepository
    {
        private readonly AppDbContext _context; 

        public ApplicationUserRepository()
        {
            _context = new AppDbContext();
        }

        public ApplicationUser? LoginWithCredentials(string email, string password)
        {
            return _context.ApplicationUsers
                .FirstOrDefault(user => user.Email == email && user.Password == password);
        }

        public ApplicationUser? FindByEmail(string email)
        {
            return _context.ApplicationUsers
                .FirstOrDefault(user => user.Email == email);
        }

        public ApplicationUser? FindById(Guid id)
        {
            return _context.ApplicationUsers
                .FirstOrDefault(user => user.Id == id);
        }

    }
}
