using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;

namespace Memorial.ViewModels
{
    public class ListQuadrangleItemsViewModel
    {
        public IEnumerable<QuadrangleItem> QuadrangleItems { get; set; }
        public QuadrangleArea QuadrangleArea { get; set; }
        public Quadrangle Quadrangle { get; set; }
    }
}