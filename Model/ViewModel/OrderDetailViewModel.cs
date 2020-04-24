using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class OrderDetailViewModel
    {
        public string ProductName { set; get; }
        public long OrderID { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
