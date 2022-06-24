using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_market.Core.Domain.Common;

namespace E_market.Core.Domain.Entities
{
    public class Category : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        //Navigation Properties
        public ICollection<Article> Articles { get; set; }
    }
}
