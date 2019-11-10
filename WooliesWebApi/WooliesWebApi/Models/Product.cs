using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WooliesWebApi.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int CustomerID {get; set;}
    }
}