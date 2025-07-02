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
    public class MiscellaneousReceiptsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MiscellaneousReceiptsController(IUnitOfWork unitOfWork)
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

        public ActionResult NonOrder(string AF, MasterCatalog MasterCatalog)
        {
            dynamic receipt = null;
            switch (MasterCatalog)
            {
                case MasterCatalog.Miscellaneous:
                    receipt = _unitOfWork.Receipts.GetByNonOrderActiveMiscellaneousAF(AF).ToList();
                    break;
            }
            
            var viewModel = new NonOrderReceiptsViewModel()
            {
                AF = AF,
                MasterCatalog = MasterCatalog,
                ReceiptDtos = Mapper.Map<IEnumerable<Receipt>, IEnumerable<ReceiptDto>>(receipt)
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

        public ActionResult NonOrderForm(string AF, MasterCatalog MasterCatalog)
        {
            float amount = 0;
            float remainingAmount = 0;
            switch (MasterCatalog)
            {
                case MasterCatalog.Miscellaneous:
                    amount = _unitOfWork.MiscellaneousTransactions.GetActive(AF).Amount;
                    remainingAmount = amount - _unitOfWork.Receipts.GetByNonOrderActiveMiscellaneousAF(AF).Sum(r => r.Amount);
                    break;
            }

            var viewModel = new NewNonOrderReceiptFormViewModel()
            {
                MasterCatalog = MasterCatalog,
                AF = AF,
                Amount = amount,
                RemainingAmount = remainingAmount,
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
                var invoice = _unitOfWork.Invoices.GetByActiveIV(viewModel.InvoiceDto.IV);
                var number = _unitOfWork.MiscellaneousNumbers.GetNewRE(_unitOfWork.MiscellaneousTransactions.GetActive(viewModel.InvoiceDto.MiscellaneousTransactionAF).MiscellaneousItemId, System.DateTime.Now.Year);
                if (number != "")
                {
                    var receipt = Mapper.Map<ReceiptDto, Receipt>(viewModel.ReceiptDto);
                    receipt.MiscellaneousTransactionAF = viewModel.InvoiceDto.MiscellaneousTransactionAF;
                    receipt.InvoiceIV = viewModel.InvoiceDto.IV;
                    receipt.RE = number;
                    receipt.CreateDate = System.DateTime.Now;
                    _unitOfWork.Receipts.Add(receipt);
                    _unitOfWork.Complete();

                    invoice.hasReceipt = true;
                    _unitOfWork.Complete();

                    var receiptTotalAmount = _unitOfWork.Receipts.GetByActiveIV(viewModel.InvoiceDto.IV).Sum(r => r.Amount);
                    if (viewModel.InvoiceDto.Amount == receiptTotalAmount)
                    {
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

        public ActionResult SaveNonOrder(NewNonOrderReceiptFormViewModel viewModel)
        {
            dynamic transaction = null;
            float amount = 0;
            float remainingAmount = 0;
            switch (viewModel.MasterCatalog)
            {
                case MasterCatalog.Miscellaneous:
                    transaction = _unitOfWork.MiscellaneousTransactions.GetActive(viewModel.AF);
                    amount = _unitOfWork.MiscellaneousTransactions.GetActive(viewModel.AF).Amount;
                    remainingAmount = amount - _unitOfWork.Receipts.GetByNonOrderActiveMiscellaneousAF(viewModel.AF).Sum(r => r.Amount);
                    break;
            }

            if (transaction.Amount < viewModel.ReceiptDto.Amount ||
                (transaction.Amount - _unitOfWork.Receipts.GetByNonOrderActiveMiscellaneousAF(viewModel.AF).Sum(r => r.Amount)) < viewModel.ReceiptDto.Amount)
            {
                
                ModelState.AddModelError("ReceiptDto.Amount", "Amount invalid");
                viewModel.PaymentMethods = _unitOfWork.PaymentMethods.GetAll();
                viewModel.Amount = amount;
                viewModel.RemainingAmount = remainingAmount;
                return View("NonOrderForm", viewModel);
            }

            if (viewModel.ReceiptDto.RE == null)
            {
                string number = "";
                switch (viewModel.MasterCatalog)
                {
                    case MasterCatalog.Miscellaneous:
                        number = _unitOfWork.MiscellaneousNumbers.GetNewRE(_unitOfWork.MiscellaneousTransactions.GetActive(viewModel.AF).MiscellaneousItemId, System.DateTime.Now.Year);
                        break;
                }

                if (number != "")
                {
                    var receipt = Mapper.Map<ReceiptDto, Receipt>(viewModel.ReceiptDto);
                    switch (viewModel.MasterCatalog)
                    {
                        case MasterCatalog.Miscellaneous:
                            receipt.MiscellaneousTransactionAF = viewModel.AF;
                            break;
                    }
                    receipt.RE = number;
                    receipt.CreateDate = System.DateTime.Now;
                    _unitOfWork.Receipts.Add(receipt);
                    _unitOfWork.Complete();
                }
            }
            else
            {
                var receipt = _unitOfWork.Receipts.GetByActiveRE(viewModel.ReceiptDto.RE);
                Mapper.Map(viewModel.ReceiptDto, receipt);
                receipt.ModifyDate = System.DateTime.Now;
                _unitOfWork.Complete();
            }

            return RedirectToAction("NonOrder", new { AF = viewModel.AF, MasterCatalog = viewModel.MasterCatalog });
        }

        public ActionResult DeleteOrderReceipt(string RE, string IV)
        {
            var receipt = _unitOfWork.Receipts.GetByActiveRE(RE);
            receipt.DeleteDate = System.DateTime.Now;
            _unitOfWork.Complete();

            if (_unitOfWork.Receipts.GetByActiveIV(IV).ToList().Count == 0)
            {
                _unitOfWork.Invoices.GetByActiveIV(IV).hasReceipt = false;
                _unitOfWork.Complete();
            }

            return RedirectToAction("Order", new { IV = IV });
        }

        public ActionResult DeleteNonOrderReceipt(string RE, string AF, MasterCatalog MasterCatalog)
        {
            var receipt = _unitOfWork.Receipts.GetByActiveRE(RE);
            receipt.DeleteDate = System.DateTime.Now;
            _unitOfWork.Complete();
            return RedirectToAction("NonOrder", new { AF = AF, MasterCatalog = MasterCatalog });
        }
    }
}