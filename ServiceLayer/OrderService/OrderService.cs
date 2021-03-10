using DataLayer;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceLayer.OrderService
{
    public class OrderService : IOrderService
    {
        private IRepository<Order> _orderRepository;
        private IProductService _productService;

        public OrderService(IRepository<Order> orderRepository, IProductService productService)
        {
            this._orderRepository = orderRepository;
            this._productService = productService;
        }

        public void DeleteOrder(int orderId)
        {
            this._orderRepository.DeleteEntity(orderId);
        }

        public List<Order> GetAllOrders()
        {
            return this._orderRepository.GetAllEntities().ToList();
        }

        public Order GetOrderById(int id)
        {
            return this._orderRepository.GetEntityById(id);
        }

        public void MakeOrder(List<Product> products, User user)
        {
            if (products == null || products.Count <= 0)
                throw new ArgumentException("Za pravljenje porudzbine morate izabrati proizvod.");
            
            this.ValidateProducts(products);
            
            Order order = new Order
            {
                OrderDate = DateTime.Now.Date,
                Products = products,
                Customer = user,
                TotalAmount = this.GetTotalAmount(products)
            };

            this._orderRepository.AddEntity(order);
        }

        #region HelperMethods
        private double GetTotalAmount(List<Product> products)
        {
            return products.
                    Select(x => x.Price).
                    Sum();
        }

        private void ValidateProducts(List<Product> products)
        {
            foreach(Product product in products)
            {
                if (!this._productService.IsProductInStock(product.Id))
                {
                    throw new ArgumentException($"Proizvod {product.Name} nije na stanju.");
                }
            }
        }
        #endregion
    }
}
