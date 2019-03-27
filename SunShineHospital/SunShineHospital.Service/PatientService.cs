using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SunShineHospital.Data.Infrastructure;
using SunShineHospital.Data.Repositories;
using SunShineHospital.Model.Models;

namespace SunShineHospital.Service
{
    public interface IPatientService
    {
        Patient Add(Patient patient);

        void Update(Patient patient);

        Patient Delete(int id);

        int GetPatientIdByUserId(string userId);

        void Save();
    }

    public class PatientService : IPatientService
    {
        private IPatientRepository _patientRepository;
        private IUnitOfWork _unitOfWork;

        public PatientService(IPatientRepository patientRepository, IUnitOfWork unitOfWork)
        {
            _patientRepository = patientRepository;
            this._unitOfWork = unitOfWork;
        }

        public Patient Add(Patient patient)
        {
            return _patientRepository.Add(patient);
        }

        public Patient Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int GetPatientIdByUserId(string userId)
        {
            return _patientRepository.GetPatientIdByUserId(userId);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Patient patient)
        {
            throw new NotImplementedException();
        }
    }
}
