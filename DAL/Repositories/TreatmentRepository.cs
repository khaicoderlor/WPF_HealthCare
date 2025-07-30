using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Entities;

namespace DAL.Repositories
{
    public class TreatmentRepository
    {
        private readonly AppDbContext _context;
        public TreatmentRepository()
        {
            _context = new AppDbContext();
        }
        public List<Service> GetAllService()
        {
            return _context.Services
                .ToList();
        }

    }
}
