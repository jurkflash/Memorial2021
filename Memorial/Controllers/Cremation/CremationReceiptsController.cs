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
    public class CremationReceiptsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CremationReceiptsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Order(string IV)
        {
            var viewModel = new OrderReceiptsViewModel()
            {
                InvoiceDto = Mapper.Map<Invoice, InvoiceDto>(_unitOfWork.Invoices.GetByActiveIV(IV)),
                ReceiptDtos = Mapper.Map<IEnumerable<Receipt>, IEnumerable<ReceiptDto>>(_unitOfWork.Receipts.GetByActiveIV(IV).ToList())
            };

            return View(viewModel);
        }

        public ActionResult NonOrder(string AF)
        {
            var viewModel = new CremationNonOrderReceiptsViewModel()
            {
                CremationTransactionDto = Mapper.Map<CremationTransaction, CremationTransactionDto>(_unitOfWork.CremationTransactions.GetActive(AF)),
                ReceiptDtos = Mapper.Map<IEnumerable<Receipt>, IEnumerable<ReceiptDto>>(_unitOfWork.Receipts.GetByNonOrderActiveCremationAF(AF).ToList())
            };

            return View(viewModel);
        }

        public ActionResult OrderForm(string IV)
        {
            var viewModel = new NewOrderReceiptFormViewModel()
            {
                InvoiceDto = Mapper.Map<Invoice, InvoiceDto>(_unitOfWork.Invoices.GetByActiveIV(IV)),
                ReceiptDto = new ReceiptDto(),
                PaymentMethods = _unitOfWork.PaymentMethods.GetAll()
            };
            return View(viewModel);
        }

        public ActionResult NonOrderForm(string AF)
        {
            var viewModel = new CremationNewNonOrderReceiptFormViewModel()
            {
                CremationTransactionDto = Mapper.Map<CremationTransaction, CremationTransactionDto>(_unitOfWork.CremationTransactions.GetActive(AF)),
                ReceiptDto = new ReceiptDto(),
                PaymentMethods = _unitOfWork.PaymentMethods.GetAll()
            };
            return View(viewModel);
        }

        public ActionResult SaveOrder(NewOrderReceiptFormViewModel viewModel)
        {
            if (viewModel.InvoiceDto.Amount < viewModel.ReceiptDto.Amount ||
                (viewModel.InvoiceDto.Amount - _unitOfWork.Receipts.GetByActiveIV(viewModel.InvoiceDto.IV).Sum(r => r.Amount)) < viewModel.ReceiptDto.Amount)
            {
                ModelState.AddModelError("ReceiptDto.Amount", "Amount invalid");
                viewModel.PaymentMethods = _unitOfWork.PaymentMethods.GetAll();
                return View("OrderForm", viewModel);
            }

            if (viewModel.ReceiptDto.RE == null)
            {
                var number = _unitOfWork.CremationNumbers.GetNewRE(_unitOfWork.CremationTransactions.GetActive(viewModel.InvoiceDto.CremationTransactionAF).CremationItemId, System.DateTime.Now.Year);
                if (number != "")
                {
                    var receipt = Mapper.Map<ReceiptDto, Receipt>(viewModel.ReceiptDto);
                    receipt.CremationTransactionAF = viewModel.InvoiceDto.CremationTransactionAF;
                    receipt.InvoiceIV = viewModel.InvoiceDto.IV;
                    receipt.RE = number;
                    receipt.CreateDate = System.DateTime.Now;
                    _unitOfWork.Receipts.Add(receipt);
                    _unitOfWork.Complete();

                    var receiptTotalAmount = _unitOfWork.Receipts.GetByActiveIV(viewModel.InvoiceDto.IV).Sum(r => r.Amount);
                    if(viewModel.InvoiceDto.Amount == receiptTotalAmount)
                    {
                        var invoice = _unitOfWork.Invoices.GetByActiveIV(viewModel.InvoiceDto.IV);
                        invoice.isPaid = true;
                        _unitOfWork.Complete();
                    }
                }
            }
            else
            {
                var receipt = _unitOfWork.Receipts.GetByActiveRE(viewModel.ReceiptDto.RE);
                Mapper.Map(viewModel.ReceiptDto, receipt);
                receipt.ModifyDate = System.DateTime.Now;
                _unitOfWork.Complete();
            }

            return RedirectToAction("Order", new { IV = viewModel.InvoiceDto.IV });
        }

        public ActionResult SaveNonOrder(CremationNewNonOrderReceiptFormViewModel viewModel)
        {
            return View();
        }

        public ActionResult DeleteOrderReceipt(string RE, string IV)
        {
            var receipt = _unitOfWork.Receipts.GetByActiveRE(RE);
            receipt.DeleteDate = System.DateTime.Now;
            _unitOfWork.Complete();
            return RedirectToAction("Order", new { IV = IV });
        }
    }
}