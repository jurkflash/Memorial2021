﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;
using Memorial.Core.Domain;

namespace Memorial.ViewModels
{
    public class NewOrderReceiptFormViewModel
    {
        public InvoiceDto InvoiceDto { get; set; }
        public ReceiptDto ReceiptDto { get; set; }
        public IEnumerable<PaymentMethod> PaymentMethods { get; set; }
    }
}