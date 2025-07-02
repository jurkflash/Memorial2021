using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;
using Memorial.ViewModels;

namespace Memorial.Controllers
{
    public class CremationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CremationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(byte siteId, int applicantId)
        {
            var viewModel = new CremationIndexesViewModel()
            {
                Cremations = _unitOfWork.Cremations.GetBySite(siteId),
                ApplicantId = applicantId
            };
            return View(viewModel);
        }

        public ActionResult Items(int cremationId, int applicantId)
        {
            var viewModel = new CremationItemsViewModel()
            {
                CremationItems = _unitOfWork.CremationItems.GetByCremation(cremationId),
                ApplicantId = applicantId
            };
            return View(viewModel);
        }

        public ActionResult Item(string item, int cremationItemId, int applicantId)
        {
            return RedirectToAction("Index", "Cremation" + item, new { cremationItemId = cremationItemId, applicantId = applicantId });
        }
    }
}