using SunShineHospital.Models;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace SunShineHospital.Infrastructure.Binders
{
    public class BookingModelBinder : IModelBinder
    {
        private const string sessionKey = "BookingInfor";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            BookingViewModel bookingViewModel = (BookingViewModel)controllerContext.HttpContext.Session[sessionKey];
           
            if (bookingViewModel == null)
            {
                bookingViewModel = new BookingViewModel();
                controllerContext.HttpContext.Session[sessionKey] = bookingViewModel;
            }

            return bookingViewModel;
        }
    }
}