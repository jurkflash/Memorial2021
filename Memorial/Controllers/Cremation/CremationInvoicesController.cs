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
    public class CremationInvoicesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CremationInvoicesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(string AF)
        {
            var viewModel = new InvoicesViewModel()
            {
                AF = AF,
                InvoiceDtos = Mapper.Map<IEnumerable<Invoice>, IEnumerable<InvoiceDto>>(_unitOfWork.Invoices.GetByActiveCremationAF(AF).ToList())
            };

            return View(viewModel);
        }

        public ActionResult New(string AF)
        {
            var viewModel = new InvoiceFormViewModel()
            {
                AF = AF,
                Amount = _unitOfWork.CremationTransactions.GetActive(AF).Price,
                InvoiceDto = new InvoiceDto()
            };

            return View("Form", viewModel);
        }

        public ActionResult Save(InvoiceFormViewModel viewModel)
        {
            if (viewModel.Amount < viewModel.InvoiceDto.Amount)
            {
                ModelState.AddModelError("InvoiceDto.Amount", "Amount invalid");
                return View("Form", viewModel);
            }

            if (viewModel.InvoiceDto.IV == null)
            {
                var number = _unitOfWork.CremationNumbers.GetNewIV(_unitOfWork.CremationTransactions.GetActive(viewModel.AF).CremationItemId, System.DateTime.Now.Year);
                if (number != "")
                {
                    var invoice = Mapper.Map<InvoiceDto, Invoice>(viewModel.InvoiceDto);
                    invoice.CremationTransactionAF = viewModel.AF;
                    invoice.IV = number;
                    invoice.CreateDate = System.DateTime.Now;
                    _unitOfWork.Invoices.Add(invoice);
                    _unitOfWork.Complete();
                }
            }
            else
            {
                var invoice = _unitOfWork.Invoices.GetByActiveIV(viewModel.InvoiceDto.IV);
                Mapper.Map(viewModel.InvoiceDto, invoice);
                invoice.ModifyDate = System.DateTime.Now;
            }

            return RedirectToAction("Index", new { AF = viewModel.AF });
        }

        public ActionResult Receipt(string IV)
        {
            return RedirectToAction("Order", "CremationReceipts", new { IV = IV});
        }

        public ActionResult Delete(string IV)
        {
            var receipts = _unitOfWork.Receipts.GetByActiveIV(IV);
            foreach(Receipt receipt in receipts)
            {
                receipt.DeleteDate = System.DateTime.Now;
            }
            var invoice = _unitOfWork.Invoices.GetByActiveIV(IV);
            invoice.DeleteDate = System.DateTime.Now;
            _unitOfWork.Complete();

            return RedirectToAction("Index", new { AF = invoice.CremationTransactionAF });
        }
    }
}