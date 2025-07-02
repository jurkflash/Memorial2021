using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;
using Memorial.Core.Dtos;
using Memorial.Core.Domain;
using Memorial.ViewModels;
using AutoMapper;

namespace Memorial.Controllers.Miscellaenous
{
    public class MiscellaneousNonOrdersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MiscellaneousNonOrdersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(int miscellaneousItemId, int applicantId)
        {
            var miscellaneousTransactionDto = Mapper.Map<IEnumerable<MiscellaneousTransaction>, IEnumerable<MiscellaneousTransactionDto>>(_unitOfWork.MiscellaneousTransactions.GetByItemAndApplicant(miscellaneousItemId, applicantId));
            var viewModel = new MiscellaneousItemIndexesViewModel()
            {
                ApplicantId = applicantId,
                MiscellaneousItemId = miscellaneousItemId,
                MiscellaneousTransactionDtos = miscellaneousTransactionDto
            };
            return View(viewModel);
        }

        public ActionResult New(int miscellaneousItemId, int applicantId)
        {
            var miscellaneousTransactionDto = new MiscellaneousTransactionDto();
            miscellaneousTransactionDto.ApplicantId = applicantId;
            miscellaneousTransactionDto.MiscellaneousItemId = miscellaneousItemId;
            return View("Form", miscellaneousTransactionDto);
        }

        public ActionResult Save(MiscellaneousTransactionDto miscellaneousTransactionDto)
        {
            if (miscellaneousTransactionDto.AF == null)
            {
                var number = _unitOfWork.MiscellaneousNumbers.GetNewAF(miscellaneousTransactionDto.MiscellaneousItemId, System.DateTime.Now.Year);

                if (number == "")
                {
                    return View("Form", miscellaneousTransactionDto);
                }
                else
                {
                    var miscellaneousTransaction = Mapper.Map<MiscellaneousTransactionDto, MiscellaneousTransaction>(miscellaneousTransactionDto);
                    miscellaneousTransaction.AF = number;
                    miscellaneousTransaction.CreateDate = System.DateTime.Now;
                    _unitOfWork.MiscellaneousTransactions.Add(miscellaneousTransaction);
                    _unitOfWork.Complete();
                }
            }
            return RedirectToAction("Index", new { miscellaneousItemId = miscellaneousTransactionDto.MiscellaneousItemId, applicantId = miscellaneousTransactionDto.ApplicantId });
        }

        public ActionResult Info(string AF)
        {
            var miscellaneousTransactionDto = Mapper.Map<MiscellaneousTransaction, MiscellaneousTransactionDto>(_unitOfWork.MiscellaneousTransactions.GetActive(AF));
            return View(miscellaneousTransactionDto);
        }

        public ActionResult Receipt(string AF)
        {
            return RedirectToAction("NonOrder", "MiscellaneousReceipts", new { AF = AF, MasterCatalog = MasterCatalog.Miscellaneous });
        }

    }
}