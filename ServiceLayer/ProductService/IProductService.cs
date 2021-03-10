using Domain;
using System.Collections.Generic;

namespace ServiceLayer
{
    public interface IProductService
    {
        void AddProduct(Product product);
        void DeleteProduct(int id);
        void UpdateProduct(Product product);
        bool IsProductInStock(int productId);
        List<Product> GetAllProducts();
        Product GetProductById(int id);
    }
}
