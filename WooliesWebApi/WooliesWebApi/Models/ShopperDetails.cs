using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WooliesWebApi.Models
{
    public class ShopperDetails
    {
        public int CustomerID { get; set; }
        public List<Products> ProductList { get; set; }
    }
    public class Products
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

    }

}