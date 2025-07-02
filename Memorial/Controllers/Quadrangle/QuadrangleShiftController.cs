using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;

namespace Memorial.Controllers
{
    public class QuadrangleShiftController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuadrangleShiftController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public ActionResult Index(int quadrangleItemId, int quadrangleId, int quadrangleAreaId)
        {
            return View();
        }
    }
}