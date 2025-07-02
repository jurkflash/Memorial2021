using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;

namespace Memorial.ViewModels
{
    public class MiscellaneousIndexesViewModel
    {
        public IEnumerable<Miscellaneous> Miscellaneous { get; set; }
        public int ApplicantId { get; set; }
    }
}