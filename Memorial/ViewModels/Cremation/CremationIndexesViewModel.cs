using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;

namespace Memorial.ViewModels
{
    public class CremationIndexesViewModel
    {
        public IEnumerable<Cremation> Cremations { get; set; }
        public int ApplicantId { get; set; }
    }
}