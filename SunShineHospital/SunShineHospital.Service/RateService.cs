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
    public interface IRateService
    {
        Rate Add(Rate rate);

        void Update(Rate rate);

        Rate Delete(int id);

        IEnumerable<Rate> GetAllByDoctorId(int doctorId);

        Rate GetCommentById(int rateId);

        Rate GetRateByUserId(string userId, int doctorId);

        void Save();
    }

    public class RateService : IRateService
    {
        private IRateRepository _rateRepository;
        private IDoctorRepository _doctorRepository;
        private IUnitOfWork _unitOfWork;

        public RateService(IRateRepository rateRepository, IDoctorRepository doctorRepository, IUnitOfWork unitOfWork)
        {
            this._rateRepository = rateRepository;
            this._doctorRepository = doctorRepository;
            this._unitOfWork = unitOfWork;
        }

        public Rate Add(Rate rate)
        {
            return _rateRepository.Add(rate);
        }

        public Rate GetRateByUserId(string userId, int doctorId)
        {
            return _rateRepository.GetRateByUserId(userId, doctorId);
        }

        public Rate Delete(int id)
        {
            return _rateRepository.Delete(id);
        }

        public IEnumerable<Rate> GetAllByDoctorId(int doctorId)
        {
            return _rateRepository.GetMulti(m => m.DoctorID == doctorId);
        }

        public Rate GetCommentById(int rateId)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Rate rate)
        {
            _rateRepository.Update(rate);
        }
    }
}
