using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class AncestorItem
    {
        public AncestorItem()
        {
            AncestorNumbers = new HashSet<AncestorNumber>();

            AncestorTransactions = new HashSet<AncestorTransaction>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public string Code { get; set; }

        public string SystemCode { get; set; }

        public AncestorArea AncestorArea { get; set; }

        public int AncestorAreaId { get; set; }

        public ICollection<AncestorNumber> AncestorNumbers { get; set; }

        public ICollection<AncestorTransaction> AncestorTransactions { get; set; }
    }
}