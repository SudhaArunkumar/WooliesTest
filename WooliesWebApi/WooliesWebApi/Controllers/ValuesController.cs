using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using WooliesWebApi.Models;
using System.Web.Http;
using Newtonsoft.Json;


namespace WooliexWebAPI.Controllers

{
    //[Authorize]
    public class AnswerController : ApiController
    {
        /// <summary>
        /// This Method is return the list of Users
        /// </summary>
        /// <returns></returns>
        /// GET api/values

        [Route("api/answer")]
        public string GetUser()
        {

            List<User> user = new List<User>();

            user.Add(new  User { Name = "Sudha",  Token = "122723-365463564-64564" });

            user.Add(new User { Name = "Arunkumar",  Token = "475684587-365463564-64564" });

            user.Add(new User { Name = "Thaswika", Token = "365467534675-365463564-64564" });

            string json = JsonConvert.SerializeObject(new { results = user });

            return json;

        }
        /// <summary>
        /// This Method is used to return the particular username and token based on username
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        // GET api/values/5

        [Route("api/answer/{user}")]

        public string GetUser(string user)
        {
            List<User> users = new List<User>();

            users.Add(new User { Name = "Sudha",  Token = "f04e63c3-7e44-436f-ac8a-1f07c49b38d8" });

            users.Add(new User { Name = "Arunkumar", Token = "475684587-365463564-64564" });

            users.Add(new User { Name = "Thaswika", Token = "365467534675-365463564-64564" });

            var useritem = users.Where(x=> x.Token == user).Select(s => s);

            string json = JsonConvert.SerializeObject(new { results = useritem });

            return json;
        }

        /// <summary>
        /// This Method is used to return the list of product based on sortOption
        /// List of SortOption
        /// "Low" - Low to High Price
        ///"High" - High to Low Price
        ///"Ascending" - A - Z sort on the Name
        ///"Descending" - Z - A sort on the Name
        ///"Recommended" - this will call the "shopperHistory" resource to get a list of customers orders and needs to return based on popularity,
        ///Your response will be in the same data structure as the "products" response
        /// </summary>
        /// <param name="sortOption"></param>
        /// <returns></returns>

        [Route("api/resource/{sortOption}")]
        public string GetProducts(string sortOption)
        {
            int previousItem = 0;
            List<Product> product = new List<Product>();

            product.Add(new Product { ProductID = 1, ProductName = "Milk", Price = 3.30, Quantity = 1 });

            product.Add(new Product { ProductID = 2, ProductName = "Paneer", Price = 6.60, Quantity = 10 });

            product.Add(new Product { ProductID = 3, ProductName = "Carrot", Price = 9.00, Quantity = 5 });

            product.Add(new Product { ProductID = 4, ProductName = "Chicken", Price = 10.00, Quantity = 8 });

            product.Add(new Product { ProductID = 5, ProductName = "Fish", Price = 18.30, Quantity = 7 });

            product.Add(new Product { ProductID = 6, ProductName = "Avacoda", Price = 3.20, Quantity = 4 });

            product.Add(new Product { ProductID = 7, ProductName = "Tomatto", Price = 5.30, Quantity = 6 });

            product.Add(new Product { ProductID = 8, ProductName = "Onion", Price = 9.30, Quantity = 7 });

            product.Add(new Product { ProductID = 9, ProductName = "Apple", Price = 2.30, Quantity = 9});

            product.Add(new Product { ProductID = 10, ProductName = "Orange", Price = 9.30, Quantity = 8 });

            product.Add(new Product { ProductID = 11, ProductName = "Grapes", Price = 11.30, Quantity = 5});

            product.Add(new Product { ProductID = 12, ProductName = "FreshCream", Price = 4.30, Quantity = 10 });

            product.Add(new Product { ProductID = 13, ProductName = "Broccoli", Price = 7.30, Quantity = 11 });

            List<Product> ListItem = new List<Product>();

            switch(sortOption)
            {
                case "Low":
                    ListItem = product.OrderBy(x => x.Price).ToList();
                    break;
                case "High":
                    ListItem = product.OrderByDescending(x => x.Price).ToList();
                    break;
                case "Ascending":
                    ListItem = product.OrderBy(x => x.ProductName).ToList();
                    break;
                case "Descending":
                    ListItem = product.OrderByDescending(x => x.ProductName).ToList();
                    break;
                case "Recommended":

                    List<Product> productList = new List<Product>();
                    List<ShopperDetails> shopperList = new List<ShopperDetails>();

                    productList.Add(new Product { ProductID = 8, ProductName = "Onion", Price = 9.30, Quantity = 7 ,CustomerID =123});

                    productList.Add(new Product { ProductID = 9, ProductName = "Apple", Price = 2.30, Quantity = 9, CustomerID = 123 });

                    productList.Add(new Product { ProductID = 10, ProductName = "Orange", Price = 9.30, Quantity = 8 , CustomerID = 124 });

                    productList.Add(new Product { ProductID = 11, ProductName = "Grapes", Price = 11.30, Quantity = 5, CustomerID = 124 });

                    var customerIDList = productList.Select(x => x.CustomerID).ToList();

                    foreach(var item in customerIDList)
                    {
                        if(previousItem !=item)
                        {
                            ShopperDetails shopperItem = new ShopperDetails();
                            shopperItem.CustomerID = item;
                            var List = productList.Where(p => p.CustomerID == item).Select(s => new Products()
                            {
                                ProductID = s.ProductID,
                                ProductName = s.ProductName,
                                Price = s.Price,
                                Quantity = s.Quantity
                            }).ToList();

                            shopperItem.ProductList = List;
                            shopperList.Add(shopperItem);
                        }
                        previousItem = item;
                    }

                    string value = JsonConvert.SerializeObject(new { results = shopperList });
                    return value;
            }

            string json = JsonConvert.SerializeObject(new { results = ListItem });
            return json;
        }

        /// <summary>
        /// This will call your api looking for a resource available at your base url /trolleyTotal which should return the 
        /// lowest possible total based on provided lists of prices, specials and quantities.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>

        // Post api/values/5
        [Route("api/resource/TrollyTotal/{token}")]
        public string GetTorllyTotal(string token)
        {
            List<CustomerProductDetails> cusProductList = new List<CustomerProductDetails>();
            cusProductList.Add(new CustomerProductDetails { ProductID = 1, ProductName = "Apple", Price = 3.00, Quantity = 1, SplProductName = "Mango", SplQuantity = 4, QuantitiesQty = 3, CustomerID = "f04e63c3-7e44-436f-ac8a-1f07c49b38d8" });
            cusProductList.Add(new CustomerProductDetails { ProductID = 2, ProductName = "Orange", Price = 2.30, Quantity = 1, SplProductName = "Panner", SplQuantity = 7, QuantitiesQty = 9, CustomerID = "f04e63c3-7e44-436f-ac8a-1f07c49b38d8" });
            cusProductList.Add(new CustomerProductDetails { ProductID = 3, ProductName = "Grapes", Price = 2.30, Quantity = 1, SplProductName = "Mango", SplQuantity = 6, QuantitiesQty = 10, CustomerID = "33333333-8888-7777-5555-1f07c49b35678" });
            var custProductList = cusProductList.Where(w=>w.CustomerID == token).OrderByDescending(x => x.Price).ThenByDescending(s => s.SplQuantity).ThenByDescending(c => c.QuantitiesQty).FirstOrDefault();

            int total = Convert.ToInt32(custProductList.Price) + custProductList.SplQuantity + custProductList.QuantitiesQty;

            string json = JsonConvert.SerializeObject(new { results = total.ToString() });

            return json;
        }



        // DELETE api/values/5

        public void Delete(int id)

        {

        }

    }

}  