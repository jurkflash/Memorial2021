using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;
using Memorial.ViewModels;

namespace Memorial.Controllers.Miscellaenous
{
    public class MiscellaneousController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MiscellaneousController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(byte siteId, int applicantId)
        {
            var viewModel = new MiscellaneousIndexesViewModel()
            {
                Miscellaneous = _unitOfWork.Miscellaneous.GetBySite(siteId),
                ApplicantId = applicantId
            };
            return View(viewModel);
        }

        public ActionResult Items(int miscellaneousId, int applicantId)
        {
            var viewModel = new MiscellaneousItemsViewModel()
            {
                MiscellaneousItems = _unitOfWork.MiscellaneousItems.GetByMiscellaneous(miscellaneousId),
                ApplicantId = applicantId
            };
            return View(viewModel);
        }

        public ActionResult Item(string item, int miscellaneousItemId, int applicantId)
        {
            if(_unitOfWork.MiscellaneousItems.Get(miscellaneousItemId).isOrder)
                return RedirectToAction("Index", "MiscellaneousOrders", new { miscellaneousItemId = miscellaneousItemId, applicantId = applicantId });
            else
                return RedirectToAction("Index", "MiscellaneousNonOrders", new { miscellaneousItemId = miscellaneousItemId, applicantId = applicantId });
        }
    }
}