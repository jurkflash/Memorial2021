using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;

namespace Memorial.ViewModels
{
    public class CremationItemsViewModel
    {
        public IEnumerable<CremationItem> CremationItems { get; set; }
        public int ApplicantId { get; set; }
    }
}