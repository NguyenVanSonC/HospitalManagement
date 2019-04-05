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
    public interface IPrescriptionService
    {
        Prescription Add(Prescription prescription);

        void Update(Prescription prescription);

        Prescription Delete(int id);

        IEnumerable<Prescription> GetAll();

        bool CreatePrescription(Prescription prescription, IEnumerable<PrescriptionDetail> listPrescriptionDetails);

        void Save();
    }

    public class PrescriptionService : IPrescriptionService
    {
        private IPrescriptionRepository _prescriptionRepository;
        private IPrescriptionDetailRepository _prescriptionDetailRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PrescriptionService(IPrescriptionRepository prescriptionRepository, IUnitOfWork unitOfWork, IPrescriptionDetailRepository prescriptionDetailRepository)
        {
            this._prescriptionRepository = prescriptionRepository;
            this._prescriptionDetailRepository = prescriptionDetailRepository;
            this._unitOfWork = unitOfWork;
        }

        public Prescription Add(Prescription prescription)
        {
            return _prescriptionRepository.Add(prescription);
        }

        public void Update(Prescription prescription)
        {
            throw new NotImplementedException();
        }

        public Prescription Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Prescription> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public bool CreatePrescription(Prescription prescription, IEnumerable<PrescriptionDetail> listPrescriptionDetails)
        {
            try
            {
                _prescriptionRepository.Add(prescription);
                _unitOfWork.Commit();
                foreach (var prescriptionDetail in listPrescriptionDetails)
                {
                    prescriptionDetail.PrescriptionID = prescription.ID;
                    _prescriptionDetailRepository.Add(prescriptionDetail);
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}
