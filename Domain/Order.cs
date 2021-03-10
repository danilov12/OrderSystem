using System;
using System.Collections.Generic;
namespace Domain
{
    public class Order : IEntity
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public User Customer { get; set; }
        public List<Product> Products { get; set; }
        public double TotalAmount { get; set; }

        public Order()
        {

        }

        public Order(int id, DateTime orderDate, User customer, List<Product> products)
        {
            this.Id = id;
            this.OrderDate = orderDate;
            this.Customer = customer;
            this.Products = products;
        }
    }
}
