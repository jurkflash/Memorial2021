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

namespace Memorial.Controllers.Cremation
{
    public class CremationOrdersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CremationOrdersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(int cremationItemId, int applicantId)
        {
            var cremationTransactionDto = Mapper.Map<IEnumerable<CremationTransaction>, IEnumerable<CremationTransactionDto>>(_unitOfWork.CremationTransactions.GetByItemAndApplicant(cremationItemId, applicantId));
            var viewModel = new CremationOrdersViewModel()
            {
                ApplicantId = applicantId,
                CremationItemId = cremationItemId,
                CremationTransactionDtos = cremationTransactionDto
            };
            return View(viewModel);
        }

        public ActionResult New(int cremationItemId, int applicantId)
        {
            var cremationTransactionDto = new CremationTransactionDto();
            cremationTransactionDto.ApplicantId = applicantId;
            cremationTransactionDto.CremationItemId = cremationItemId;
            var viewModel = new CremationOrderFormViewModel()
            {
                FuneralCompanyDtos = Mapper.Map<IEnumerable<FuneralCompany>, IEnumerable<FuneralCompanyDto>>(_unitOfWork.FuneralCompanies.GetAllActive()),
                DeceasedBriefDtos = Mapper.Map<IEnumerable<Deceased>, IEnumerable<DeceasedBriefDto>>(_unitOfWork.Deceaseds.GetByApplicant(applicantId)),
                CremationTransactionDto = cremationTransactionDto
            };
            return View("Form", viewModel);
        }

        public ActionResult Save(CremationOrderFormViewModel viewModel)
        {
            if (viewModel.CremationTransactionDto.AF == null)
            {
                var number = _unitOfWork.CremationNumbers.GetNewAF(viewModel.CremationTransactionDto.CremationItemId, System.DateTime.Now.Year);

                if (number == "")
                {
                    viewModel.FuneralCompanyDtos = Mapper.Map<IEnumerable<FuneralCompany>, IEnumerable<FuneralCompanyDto>>(_unitOfWork.FuneralCompanies.GetAllActive());
                    viewModel.DeceasedBriefDtos = Mapper.Map<IEnumerable<Deceased>, IEnumerable<DeceasedBriefDto>>(_unitOfWork.Deceaseds.GetByApplicant(viewModel.CremationTransactionDto.ApplicantId));
                    return View("Form", viewModel);
                }
                else
                {
                    var cremationTransaction = Mapper.Map<CremationTransactionDto, CremationTransaction>(viewModel.CremationTransactionDto);
                    cremationTransaction.AF = number;
                    cremationTransaction.CreateDate = System.DateTime.Now;
                    _unitOfWork.CremationTransactions.Add(cremationTransaction);
                    _unitOfWork.Complete();
                }
            }
            return RedirectToAction("Index", new { cremationItemId = viewModel.CremationTransactionDto.CremationItemId, applicantId = viewModel.CremationTransactionDto.ApplicantId });
        }

        public ActionResult Info(string AF)
        {
            var cremationTransactionDto = Mapper.Map<CremationTransaction, CremationTransactionDto>(_unitOfWork.CremationTransactions.GetActive(AF));
            return View(cremationTransactionDto);
        }

        public ActionResult Invoice(string AF)
        {
            return RedirectToAction("Index", "CremationInvoices", new { AF = AF });
        }
    }
}