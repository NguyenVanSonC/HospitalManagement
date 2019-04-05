using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AutoMapper;
using Microsoft.AspNet.Identity;
using SunShineHospital.Infrastructure.Extensions;
using SunShineHospital.Model.Models;
using SunShineHospital.Models;
using SunShineHospital.Service;

namespace SunShineHospital.Controllers
{
    public class CommentController : Controller
    {
        private ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            this._commentService = commentService;
        }
        
        public PartialViewResult Index(int? id)
        {
            return PartialView((int?)id);
        }

        public JsonResult GetComment(int commentId)
        {
            var commentModel = _commentService.GetCommentById(commentId);
            var commentViewModel = Mapper.Map<Comment, CommentViewModel>(commentModel);
            if (commentViewModel != null)
            {
                return Json(new
                {
                    status = true,
                    data = commentViewModel
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    status = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetAll(int doctorId)
        {
            var commentModel = _commentService.GetAllByDoctorId(doctorId);
            var listCommentViewModel = Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentViewModel>>(commentModel);
            return Json(new
            {
                data = listCommentViewModel,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteComment(int commentId)
        {
            var commentModel = _commentService.Delete(commentId);
            if (commentModel != null)
            {
                _commentService.Save();
                return Json(new
                {
                    status = true
                });
            }
            else
            {
                return Json(new
                {
                    status = false
                });
            }
        }

        [HttpPost]
        public JsonResult UpdateComment(string commentViewModel)
        {
            var comment = new JavaScriptSerializer().Deserialize<CommentViewModel>(commentViewModel);
            var commentNew = _commentService.GetCommentById(comment.ID);
            commentNew.Content = comment.Content;
            //bool CheckUserComment(Comment m) => m.UserId == User.Identity.GetUserId();
            if (Request.IsAuthenticated)
            {
                commentNew.UpdatedBy = User.Identity.GetUserName();
                commentNew.UpdatedDate = DateTime.Now;
                _commentService.Update(commentNew);
                _commentService.Save();
                return Json(new
                {
                    status = true
                });
            }
            else
            {
                return Json(new
                {
                    status = false
                });
            }
        }

        [HttpPost]
        public JsonResult CreateComment(string commentViewModel)
        {
            var comment = new JavaScriptSerializer().Deserialize<CommentViewModel>(commentViewModel);
            var commentNew = new Comment();
            commentNew.UpdateComment(comment);
            if (Request.IsAuthenticated)
            {
                commentNew.UserId = User.Identity.GetUserId();
                commentNew.CreatedBy = User.Identity.GetUserName();
                commentNew.CreatedDate = DateTime.Now;
                _commentService.Add(commentNew);
                _commentService.Save();
                return Json(new
                {
                    status = true
                });
            }
            else
            {
                return Json(new
                {
                    status = false
                });
            }
        }
    }
}