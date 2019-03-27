using SunShineHospital.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using SunShineHospital.Data.Infrastructure;
using SunShineHospital.Data.Repositories;

namespace SunShineHospital.Service
{
    public interface IDoctorService
    {
        Doctor Add(Doctor Doctor);

        void Update(Doctor Doctor);

        Doctor Delete(int id);

        IEnumerable<Doctor> GetAll();

        IEnumerable<Doctor> GetAll(string keyword);

        IEnumerable<Doctor> GetLastest(int top);

        IEnumerable<Doctor> GetListDoctorByDepartmentIdPaging(int categoryId, int page, int pageSize, string soft, out int totalRow);

        IEnumerable<string> GetListDoctorByName(string name);

        IEnumerable<Doctor> Search(string keyword, int page, int pageSize, string sort, out int totalRow);

        IEnumerable<Doctor> GetHotDoctor(int top);

        Doctor GetById(int id);



        void Save();
    }

    public class DoctorService : IDoctorService
    {
        private IDoctorRepository _doctorRepository;
        private IUnitOfWork _unitOfWork;
        public DoctorService(IDoctorRepository doctorRepository, IUnitOfWork unitOfWork)
        {
            this._doctorRepository = doctorRepository;
            this._unitOfWork = unitOfWork;
        }
        public Doctor Add(Doctor Doctor)
        {
            return _doctorRepository.Add(Doctor);
        }

        public Doctor Delete(int id)
        {
            return _doctorRepository.Delete(id);
        }

        public IEnumerable<Doctor> GetAll()
        {
            return _doctorRepository.GetAll();
        }

        public IEnumerable<Doctor> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return _doctorRepository.GetMulti(x => x.User.FullName.Contains(keyword) && x.Status);
            }
            else
            {
                return _doctorRepository.GetAll();
            }
            
        }

        public Doctor GetById(int id)
        {
            return _doctorRepository.GetSingleByCondition(x => x.ID == id, new string[] {"User", "Department"});
        }

        public IEnumerable<Doctor> GetHotDoctor(int top)
        {
            var listHotDoctor = _doctorRepository.GetAll(new string[]{"User", "Department"}).OrderByDescending(x => x.CreatedDate).Take(top);
            return listHotDoctor;
        }

        public IEnumerable<Doctor> GetLastest(int top)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Doctor> GetListDoctorByDepartmentIdPaging(int categoryId, int page, int pageSize, string soft, out int totalRow)
        {
            var query = _doctorRepository.GetMulti(x => x.Status && x.DepartmentID == categoryId, new string[] { "User", "Department" });
            switch (soft)
            {
                case "rate":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;
                case "men":
                    query = query.Where(x => x.User.Gender == true);
                    break;
                case "women":
                    query = query.Where(x => x.User.Gender == false);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<string> GetListDoctorByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<Doctor> Search(string keyword, int page, int pageSize, string soft, out int totalRow)
        {
            var query = _doctorRepository.GetMulti(x => x.Status, new string[]{"User", "Department"});
            switch (soft)
            {
                case "rate":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;
                case "men":
                    query = query.Where(x => x.User.Gender == true);
                    break;
                case "women":
                    query = query.Where(x => x.User.Gender == false);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.User.FullName.Contains(keyword));
                totalRow = query.Count();
                return query.Skip((page - 1) * pageSize).Take(pageSize);
            }
            else
            {
                totalRow = query.Count();
                return query.Skip((page - 1) * pageSize).Take(pageSize);
            }
        }

        public void Update(Doctor Doctor)
        {
            _doctorRepository.Update(Doctor);
        }
    }
}
