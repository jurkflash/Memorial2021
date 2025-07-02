using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;
using Memorial.ViewModels;

namespace Memorial.Controllers
{
    public class UrnController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UrnController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(byte siteId, int applicantId)
        {
            var urns =  _unitOfWork.Urns.GetBySite(siteId);
            return View(urns);
        }

        public ActionResult Items(int urnId)
        {
            var urnItems = _unitOfWork.UrnItems.GetByUrn(urnId);
            return View(urnItems);
        }

        public ActionResult Item(string item, int urnItemId)
        {
            return RedirectToAction("Index", "Urn" + item, new { urnItemId = urnItemId });
        }


        public ActionResult Transaction(int itemId)
        {
            var urnTransactions = _unitOfWork.UrnTransactions.GetByItem(itemId);
            var urnItem = _unitOfWork.UrnItems.Get(itemId);

            var urnTransactionViewModel = new ListUrnTransactionViewModel()
            {
                UrnTransactions = urnTransactions,
                UrnItem = urnItem
            };
            return View(urnTransactionViewModel);
        }

        public ActionResult New(int itemId)
        {

            return View();
        }
    }
}