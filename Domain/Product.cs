
namespace Domain
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int NumberOfProductsInStock { get; set; }

        public Product()
        {

        }

        public Product(int id, string name, double price, int numberOdProductsInStock)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
            this.NumberOfProductsInStock = numberOdProductsInStock;
        }
    }
}
