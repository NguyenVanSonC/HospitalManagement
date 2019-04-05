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
    public interface IAppoinmentService
    {
        Appoinment Add(Appoinment appoinment);

        void Update(Appoinment appoinment);

        Appoinment Delete(int id);

        Appoinment GetById(int id);

        IEnumerable<Appoinment> GetAll();

        IEnumerable<Appoinment> GetByDoctorId(int doctorId);

        bool CheckAppoinmentByPatientId(int? patientId);

        Appoinment GetAppoinmentByPatientId(int? patientId);

        IEnumerable<Appoinment> GetAppoinmentByDoctorId(int doctorId, string keyword, int page, int pageSize, string soft, out int totalRow);

        void Save();
    }
    public class AppoinmentService : IAppoinmentService
    {
        private IAppoinmentRepository _appoinmentRepository;
        private IUnitOfWork _unitOfWork;

        public AppoinmentService(IAppoinmentRepository appoinmentRepository, IUnitOfWork unitOfWork)
        {
            this._appoinmentRepository = appoinmentRepository;
            this._unitOfWork = unitOfWork;
        }
        public Appoinment Add(Appoinment appoinment)
        {
            return _appoinmentRepository.Add(appoinment);
        }

        public Appoinment Delete(int id)
        {
            return _appoinmentRepository.Delete(id);
        }

        public IEnumerable<Appoinment> GetAll()
        {
            return _appoinmentRepository.GetAll();
        }

        public bool CheckAppoinmentByPatientId(int? patientId)
        {
            var appoiment = _appoinmentRepository.GetAppoinmentByPatientId(patientId);
            return appoiment != null ? true : false;
        }

        public IEnumerable<Appoinment> GetByDoctorId(int doctorId)
        {
            var today = DateTime.Today;
            return _appoinmentRepository.GetByDoctorId(doctorId);

        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Appoinment appoinment)
        {
            _appoinmentRepository.Update(appoinment);
        }

        public Appoinment GetAppoinmentByPatientId(int? patientId)
        {
            return _appoinmentRepository.GetAppoinmentByPatientId(patientId);
        }

        public IEnumerable<Appoinment> GetAppoinmentByDoctorId(int doctorId, string keyword, int page, int pageSize, string soft, out int totalRow)
        {
            var listAppointment = _appoinmentRepository.GetAppoinmentByDoctorId(doctorId);
            totalRow = listAppointment.Count();
            return listAppointment.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public Appoinment GetById(int id)
        {
            return _appoinmentRepository.GetSingleById(id);
        }
    }
}
