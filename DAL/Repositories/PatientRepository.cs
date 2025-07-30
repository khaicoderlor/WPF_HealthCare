using DAL.Context;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
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
                .FirstOrDefault(patient => patient.ApplicationUserId == id);
        }

        public Patient? GetByUserId(Guid applicationUserId)
        {
            return _context.Patients
                           .Include(p => p.ApplicationUser)
                           .FirstOrDefault(p => p.ApplicationUserId == applicationUserId);
        }

        public Patient? GetById(Guid id)
        {
            return _context.Patients
                           .Include(p => p.ApplicationUser)
                           .FirstOrDefault(p => p.Id == id);
        }

        public void Update(Patient patient)
        {
            _context.Patients.Update(patient);
            _context.SaveChanges();
        }

        public void Add(Patient patient)
        {
            _context.Patients.Add(patient);
            _context.SaveChanges();
        }
    }
}
