using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;

namespace Memorial.Controllers
{
    public class QuadrangleManageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuadrangleManageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public ActionResult Index(int quadrangleItemId, int quadrangleId, int quadrangleAreaId)
        {
            return View();
        }
    }
}