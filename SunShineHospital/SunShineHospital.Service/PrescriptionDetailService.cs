using SunShineHospital.Data.Infrastructure;
using SunShineHospital.Data.Repositories;
using SunShineHospital.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunShineHospital.Service
{
    public interface IPrescriptionDetailService
    {
        PrescriptionDetail Add(PrescriptionDetail prescriptionDetail);

        void Update(PrescriptionDetail prescriptionDetail);

        PrescriptionDetail Delete(int id);

        IEnumerable<PrescriptionDetail> GetAll();

        void Save();
    }

    public class PrescriptionDetailService : IPrescriptionDetailService
    {
        private readonly IPrescriptionDetailRepository _prescriptionDetailRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PrescriptionDetailService(IPrescriptionDetailRepository prescriptionDetailRepository, IUnitOfWork unitOfWork)
        {
            this._prescriptionDetailRepository = prescriptionDetailRepository;
            this._unitOfWork = unitOfWork;
        }

        public PrescriptionDetail Add(PrescriptionDetail prescriptionDetail)
        {
            return _prescriptionDetailRepository.Add(prescriptionDetail);
        }

        public PrescriptionDetail Delete(int id)
        {
            return _prescriptionDetailRepository.Delete(id);
        }

        public IEnumerable<PrescriptionDetail> GetAll()
        {
            return _prescriptionDetailRepository.GetAll();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(PrescriptionDetail prescriptionDetail)
        {
            throw new NotImplementedException();
        }
    }
}
