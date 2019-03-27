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
    public interface IDepartmentService
    {
        Department Add(Department Department);

        void Update(Department Department);

        Department Delete(int id);

        IEnumerable<Department> GetAll();

        IEnumerable<Department> GetAll(string keyword);

        Department GetById(int id);

        void Save();
    }
    public class DepartmentService : IDepartmentService
    {
        #region InitialSerice
        private IUnitOfWork _unitOfWork;
        private IDepartmentRepository _departmentRepository;

        public DepartmentService(IUnitOfWork unitOfWork, IDepartmentRepository departmentRepository)
        {
            this._unitOfWork = unitOfWork;
            this._departmentRepository = departmentRepository;
        }
        #endregion

        public Department Add(Department Department)
        {
            return _departmentRepository.Add(Department);
        }

        public Department Delete(int id)
        {
            return _departmentRepository.Delete(id);
        }

        public IEnumerable<Department> GetAll()
        {
            return _departmentRepository.GetAll();
        }

        public IEnumerable<Department> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return _departmentRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            }
            else
            {
                return _departmentRepository.GetAll();
            }
        }

        public Department GetById(int id)
        {
            return _departmentRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Department Department)
        {
            _departmentRepository.Update(Department);
        }
    }
}
