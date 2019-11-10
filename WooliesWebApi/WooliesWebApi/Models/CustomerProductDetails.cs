using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WooliesWebApi.Models
{
    public class CustomerProductDetails
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string SplProductName { get; set; }
        public int SplQuantity { get; set; }
        public int QuantitiesQty { get; set; }
        public string CustomerID { get; set; }
    }
}