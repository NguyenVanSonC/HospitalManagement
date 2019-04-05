using SunShineHospital.Data.Infrastructure;
using SunShineHospital.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SunShineHospital.Data.Repositories;

namespace SunShineHospital.Service
{
    public interface IMedicineCategoryService
    {
        MedicineCategory Add(MedicineCategory medicineCategory);

        void Update(MedicineCategory medicineCategory);

        MedicineCategory Delete(int id);

        IEnumerable<MedicineCategory> GetAll();

        void Save();
    }

    public class MedicineCategoryService : IMedicineCategoryService
    {
        private IMedicineCategoryRepository _medicineCategoryRepository;
        private IUnitOfWork _unitOfWork;

        public MedicineCategoryService(IMedicineCategoryRepository medicineCategoryRepository, IUnitOfWork unitOfWork)
        {
            this._medicineCategoryRepository = medicineCategoryRepository;
            this._unitOfWork = unitOfWork;
        }

        public MedicineCategory Add(MedicineCategory medicineCategory)
        {
            return _medicineCategoryRepository.Add(medicineCategory);
        }

        public MedicineCategory Delete(int id)
        {
            return _medicineCategoryRepository.Delete(id);
        }

        public IEnumerable<MedicineCategory> GetAll()
        {
            return _medicineCategoryRepository.GetMulti(m => m.Status);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(MedicineCategory medicineCategory)
        {
            _medicineCategoryRepository.Update(medicineCategory);
        }
    }
}
