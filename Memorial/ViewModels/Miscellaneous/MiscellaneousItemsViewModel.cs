using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;

namespace Memorial.ViewModels
{
    public class MiscellaneousItemsViewModel
    {
        public IEnumerable<MiscellaneousItem> MiscellaneousItems { get; set; }
        public int ApplicantId { get; set; }
    }
}