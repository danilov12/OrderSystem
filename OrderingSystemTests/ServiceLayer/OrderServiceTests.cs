using DataLayer;
using Domain;
using NSubstitute;
using NUnit.Framework;
using ServiceLayer;
using ServiceLayer.OrderService;
using System;
using System.Collections.Generic;

namespace OrderingSystemTests.ServiceLayer
{
    [TestFixture]
    public class OrderServiceTests
    {
        private IRepository<Order> _orderRepository;
        private IProductService _productService;
        private OrderService _orderService;

        [SetUp]
        public void SetUp()
        {
            this._orderRepository = Substitute.For<IRepository<Order>>();
            this._productService = Substitute.For<IProductService>();
            this._orderService = new OrderService(this._orderRepository, this._productService);
        }

        [Test]
        public void MakeOrder_WhenProductListIsNull_ShouldThrowArgumentException()
        {
            // Arrange
            List<Product> products = null;

            // Act + Assert
            Assert.Throws<ArgumentException>(() => this._orderService.MakeOrder(products, this.GetUser()), "Za pravljenje porudzbine morate izabrati proizvod.");
        }

        [Test]
        public void MakeOrder_WhenProductListIsEmpty_ShouldThrowArgumentException()
        {
            // Arrange
            List<Product> products = new List<Product>();

            // Act + Assert
            Assert.Throws<ArgumentException>(() => this._orderService.MakeOrder(products, this.GetUser()), "Za pravljenje porudzbine morate izabrati proizvod.");
        }

        [Test]
        public void MakeOrder_WhenProductsAreNotValid_ShouldThrowArgumentException()
        {
            // Arrange
            List<Product> products = this.GetProducts();
            products.Add(new Product()
            {
                Id = 1,
                Name = "Product3",
                Price = 1,
                NumberOfProductsInStock = 0
            });

            // Act + Assert
            Assert.Throws<ArgumentException>(() => this._orderService.MakeOrder(products, this.GetUser()), $"Proizvod {products[2].Name} nije na stanju.");
        }

        [Test]
        public void MakeOrder_Success()
        {
            // Arrange
            List<Product> products = this.GetProducts();
            this._productService.IsProductInStock(1).Returns(true);
            this._productService.IsProductInStock(2).Returns(true);

            // Act
            this._orderService.MakeOrder(products, this.GetUser());

            // Assert
            this._orderRepository.Received(1).AddEntity(Arg.Is<Order>(x =>
                x.OrderDate == DateTime.Now.Date &&
                this.AreProductListsEqual(products, x.Products) &&
                this.AreCustomersEqual(this.GetUser(), x.Customer) &&
                x.TotalAmount == 45));
        }

        #region HelperMethods
        private List<Product> GetProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Name = "Product1",
                    Price = 22.5,
                    NumberOfProductsInStock = 2
                },
                new Product()
                {
                    Id = 2,
                    Name = "Product2",
                    Price = 22.5,
                    NumberOfProductsInStock = 3
                }
            };
        }

        private User GetUser()
        {
            return new User()
            {
                Id = 1
            };
        }

        private bool AreProductListsEqual(List<Product> expected, List<Product> actual)
        {
            if (expected.Count != actual.Count)
            {
                return false;
            }

            for(int i=0; i<expected.Count; i++)
            {
                if (expected[i].Id != actual[i].Id || expected[i].Name != actual[i].Name ||
                    expected[i].NumberOfProductsInStock != actual[i].NumberOfProductsInStock ||
                    expected[i].Price != expected[i].Price)
                {
                    return false;
                }
            }

            return true;
        }

        private bool AreCustomersEqual(User expected, User actual)
        {
            return expected.Id == actual.Id;
        }
        #endregion
    }
}
