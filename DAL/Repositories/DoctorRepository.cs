using DAL.Context;
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

    }
}
