using SunShineHospital.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SunShineHospital.Data.Infrastructure;
using SunShineHospital.Data.Repositories;

namespace SunShineHospital.Service
{
    public interface ICommentService
    {
        Comment Add(Comment comment);

        void Update(Comment comment);

        Comment Delete(int id);

        IEnumerable<Comment> GetAllByDoctorId(int doctorId);

        Comment GetCommentById(int commentId);

        void Save();
    }
    public class CommentService : ICommentService
    {
        private ICommentRepository _commentRepository;
        private IDoctorRepository _doctorRepository;
        private IUnitOfWork _unitOfWork;

        public CommentService(ICommentRepository commentRepository,IDoctorRepository doctorRepository, IUnitOfWork unitOfWork)
        {
            this._commentRepository = commentRepository;
            this._doctorRepository = doctorRepository;
            this._unitOfWork = unitOfWork;
        }

        public Comment Add(Comment comment)
        {
            return _commentRepository.Add(comment);
        }

        public Comment Delete(int id)
        {
            return _commentRepository.Delete(id);
        }

        public IEnumerable<Comment> GetAllByDoctorId(int doctorId)
        {
            return _commentRepository.GetMulti(m => m.DoctorID == doctorId, new string[]{ "User" }).OrderBy(m => m.CreatedDate);
        }

        public Comment GetCommentById(int commentId)
        {
            return _commentRepository.GetSingleById(commentId);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Comment comment)
        {
            _commentRepository.Update(comment);
        }
    }
}
