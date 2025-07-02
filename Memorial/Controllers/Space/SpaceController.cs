using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;
using Memorial.ViewModels;

namespace Memorial.Controllers
{
    public class SpaceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SpaceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(byte siteId, int applicantId)
        {
            var spaces = _unitOfWork.Spaces.GetBySite(siteId);
            return View(spaces);
        }

        public ActionResult Items(int spaceId)
        {
            var spaceItems = _unitOfWork.SpaceItems.GetBySpace(spaceId);
            return View(spaceItems);
        }

        public ActionResult Item(string item, int spaceItemId)
        {
            return RedirectToAction("Index", "Space" + item, new { spaceItemId = spaceItemId });
        }
    }
}