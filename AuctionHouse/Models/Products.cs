using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Models
{
    public class Products
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal MinPrice { get; set; }
        public decimal SoldPrice { get; set; }
        public int MaxLength { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
    }
}
