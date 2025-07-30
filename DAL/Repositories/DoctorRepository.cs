using DAL.Context;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class DoctorRepository
    {
        private readonly AppDbContext _context;

        public DoctorRepository()
        {
            _context = new AppDbContext();
        }

        public Doctor? FindById(Guid id)
        {
            return _context.Doctors
                .FirstOrDefault(doctor => doctor.ApplicationUserId == id);
        }

    }
}
