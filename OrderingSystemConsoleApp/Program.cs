using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Cache;

namespace OrderingSystemConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Product> p = new List<Product>()
            {
                new Product() {Id=1},
                new Product() {Id=2},
            };
            Console.WriteLine(p.Max(x => x.Id));
        }
    }

    public class Proba
    {
        ICache cache;

        public Proba()
        {
            cache = CacheSinglton.GetCache();
            cache.AddUpdateCache("danilo", new User() { Name = "Danilo", UserName = "choda" });
        }

        public string GetUserName()
        {
            return (this.cache.GetValue("danilo") as User).UserName;
        }
    }

    public class Proba2
    {
        ICache cache;

        public Proba2()
        {
            cache = CacheSinglton.GetCache();
        }

        public string GetUserName()
        {
            return (this.cache.GetValue("danilo") as User).UserName;
        }
    }
}
