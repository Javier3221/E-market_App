using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_market.Core.Domain.Entities
{
    public class Article
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }

        public int CategoryId { get; set; }
        public int UserId { get; set; }

        //Navigation Properties
        public Category Category { get; set; }
        public User User { get; set; }
    }
}
