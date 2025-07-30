using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services
{
    public class PatientService
    {
        private readonly PatientRepository _repository;

        public PatientService()
        {
            _repository = new PatientRepository();
        }

        public Patient? GetByUserId(Guid applicationUserId)
        {
            return _repository.GetByUserId(applicationUserId);
        }

        public Patient? GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public void Update(Patient patient)
        {
            _repository.Update(patient);
        }

        public void Add(Patient patient)
        {
            _repository.Add(patient);
        }
        public Guid GetPatientIdbyUserId(Guid userId)
        {
            var patient = _repository.GetByUserId(userId);
            return patient.Id;
        }
    }
}
