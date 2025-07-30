using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Entities;

namespace DAL.Repositories
{
    public class TreatmentStepRepository
    {
        private readonly AppDbContext _context;
        public TreatmentStepRepository()
        {
            _context = new AppDbContext();
        }
        public List<ServiceStep> GetStepbyServiceId(int serviceid)
        {
            return _context.ServiceSteps.Where(s => s.ServiceId == serviceid)
                .ToList();
        }
    }
}
