using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;

namespace Memorial.Controllers
{
    public class SpaceBookingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SpaceBookingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public ActionResult Index(int spaceItemId)
        {
            return View();
        }
    }
}