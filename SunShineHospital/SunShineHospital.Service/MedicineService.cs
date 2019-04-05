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
    public interface IMedicineService
    {
        Medicine Add(Medicine medicine);

        void Update(Medicine medicine);

        Medicine Delete(int id);

        IEnumerable<Medicine> GetAll();

        IEnumerable<Medicine> GetByCategoryId(int categoryId);

        Medicine GetById(int id);

        void Save();
    }

    public class MedicineService : IMedicineService
    {
        private IMedicineRepository _medicineRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MedicineService(IMedicineRepository medicineRepository, IUnitOfWork unitOfWork)
        {
            this._medicineRepository = medicineRepository;
            this._unitOfWork = unitOfWork;
        }

        public Medicine Add(Medicine medicine)
        {
            throw new NotImplementedException();
        }

        public Medicine Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Medicine> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Medicine> GetByCategoryId(int categoryId)
        {
            return _medicineRepository.GetMulti(m => m.CategoryID == categoryId && m.Status);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Medicine medicine)
        {
            throw new NotImplementedException();
        }

        public Medicine GetById(int id)
        {
            return _medicineRepository.GetSingleById(id);
        }
    }
}
