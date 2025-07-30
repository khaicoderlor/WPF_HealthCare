using DAL.Context;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PatientRepository
    {
        private readonly AppDbContext _context;

        public PatientRepository()
        {
            _context = new AppDbContext();
        }

        public Patient? FindById(Guid id)
        {
            return _context.Patients
                .FirstOrDefault(patient => patient.Id == id);
        }
    }
}
