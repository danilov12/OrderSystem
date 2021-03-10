using DataLayer;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceLayer.ProductService
{
    public class ProductService : IProductService
    {
        private IRepository<Product> _productRepository;

        public ProductService(IRepository<Product> productService)
        {
            this._productRepository = productService;
        }

        public void AddProduct(Product product)
        {
            if (string.IsNullOrEmpty(product.Name))
                throw new ArgumentException("Morate uneti ime proizvoda.");
            if (product.Price <= 0)
                throw new ArgumentException("Morate uneti cenu za proizvod.");

            this._productRepository.AddEntity(product);
        }

        public void DeleteProduct(int id)
        {
            this._productRepository.DeleteEntity(id);
        }

        public List<Product> GetAllProducts()
        {
            return this._productRepository.GetAllEntities().ToList();
        }

        public Product GetProductById(int id)
        {
            return this._productRepository.GetEntityById(id);
        }

        public bool IsProductInStock(int productId)
        {
            return this._productRepository.GetEntityById(productId).NumberOfProductsInStock != 0;
        }

        public void UpdateProduct(Product product)
        {
            if (product.Id <= 0)
                throw new ArgumentException("Proizvod nije moguce izmeniti");
            if (string.IsNullOrEmpty(product.Name))
                throw new ArgumentException("Morate uneti ime proizvoda.");
            if (product.Price <= 0)
                throw new ArgumentException("Morate uneti cenu za proizvod.");

            this._productRepository.UpdateEntity(product);
        }
    }
}
