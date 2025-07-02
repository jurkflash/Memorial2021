using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;
using Memorial.ViewModels;

namespace Memorial.Controllers
{
    public class QuadrangleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuadrangleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(byte siteId, int applicantId)
        {
            var quadrangleCentres = _unitOfWork.QuadrangleCentres.GetBySite(siteId);
            return View(quadrangleCentres);
        }

        public ActionResult Area(int quadrangleCentreId)
        {
            var quadrangleAreas = _unitOfWork.QuadrangleAreas.GetByCentre(quadrangleCentreId);
            return View(quadrangleAreas);
        }

        public ActionResult Quadrangle(int quadrangleAreaId)
        {
            var quadrangles = _unitOfWork.Quadrangles.GetByArea(quadrangleAreaId);
            return View(quadrangles);
        }

        public ActionResult Items(int quadrangleId, int quadrangleAreaId)
        {
            var quadrangleArea = _unitOfWork.QuadrangleAreas.Get(quadrangleAreaId);
            var listQuadrangleItemsViewModel = new ListQuadrangleItemsViewModel()
            {
                QuadrangleItems = _unitOfWork.QuadrangleItems.GetByCentre((int)quadrangleArea.QuadrangleCentreId),
                Quadrangle = _unitOfWork.Quadrangles.Get(quadrangleId),
                QuadrangleArea = quadrangleArea
            };

            return View(listQuadrangleItemsViewModel);
        }

        public ActionResult Item(string item, int quadrangleItemId, int quadrangleId, int quadrangleAreaId)
        {
            return RedirectToAction("Index", "Quadrangle" + item, new { quadrangleItemId = quadrangleItemId, quadrangleId = quadrangleId, quadrangleAreaId = quadrangleAreaId });
        }
    }
}