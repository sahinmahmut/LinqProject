using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqProject
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Category> categories = new List<Category>
            {
                new Category{CategoryId=1,CategoryName="Bilgisayar"},
                new Category{CategoryId=2,CategoryName="Telefon"}
            };

            List<Product> products = new List<Product>
            {
                new Product{ProductId=1,CategoryId=1, ProductName="Acer Laptop",QuantityPerUnit="32 GB RAM ", UnitInStock=5,UnitPrice=10000},
                new Product{ProductId=2,CategoryId=1, ProductName="Asus Laptop",QuantityPerUnit="16 GB RAM ", UnitInStock=3,UnitPrice=8000},
                new Product{ProductId=3,CategoryId=1, ProductName="Hp Laptop",QuantityPerUnit="8 GB RAM ", UnitInStock=2,UnitPrice=6000},
                new Product{ProductId=4,CategoryId=2, ProductName="Samsung Telefon",QuantityPerUnit="4 GB RAM ", UnitInStock=15,UnitPrice=5000},
                new Product{ProductId=5,CategoryId=2, ProductName="Apple Telefon",QuantityPerUnit="4 GB RAM ", UnitInStock=0,UnitPrice=8000},
                new Product{ProductId=6,CategoryId=2, ProductName="Xiaomi Telefon",QuantityPerUnit="128 GB RAM ", UnitInStock=5555,UnitPrice=1}
            };
            /*
            Console.WriteLine("Algoritmik--------------------------");
            GetProducts(products);// Linq kullanılmadan yazılan metot
            Console.WriteLine("Linq--------------------------");
            GetProductsLinq(products);// Linq kullanılarak yazılan metot
            Console.ReadLine();
            */


            //AnyTest(products);

            //FindTest(products);

            //FindAllTest(products);

            //ClassicLinqTest(products);

            var result = from p in products
                         join c in categories
                         on p.CategoryId equals c.CategoryId
                         where p.UnitPrice > 5000
                         orderby p.UnitPrice descending
                         select new ProductDto { ProductId = p.ProductId, ProductName = p.ProductName, CategoryName = c.CategoryName, UnitPrice = p.UnitPrice };
            foreach (var productDto in result)
            {
                Console.WriteLine("{0} ---- {1} | Price = {2} $ ", productDto.ProductName, productDto.CategoryName, productDto.UnitPrice);
            }

        }

        private static void ClassicLinqTest(List<Product> products)
        {
            var result = from p in products
                         where p.UnitPrice > 6000 && p.UnitInStock > 3
                         orderby p.UnitPrice descending, p.ProductName ascending
                         select new ProductDto { ProductId = p.ProductId, ProductName = p.ProductName, UnitPrice = p.UnitPrice };


            foreach (var product in result)
            {
                Console.WriteLine(product.ProductName);
            }
        }

        private static void FindAllTest(List<Product> products)
        {
            var result = products.FindAll(p => p.ProductName.Contains("Telefon"));
            foreach (var product in result)
            {
                Console.WriteLine(product.ProductName);
            }
        }

        private static void FindTest(List<Product> products)
        {
            var result = products.Find(p => p.ProductId == 7);
            Console.WriteLine(result != null ? result.ProductName : "Upps...Searched ID was not found.");
        }

        private static void AnyTest(List<Product> products)
        {
            var result = products.Any(p => p.ProductName == "Dell Laptop");
            Console.WriteLine(result);
        }

        static List<Product> GetProducts(List<Product> products)
        {
            List<Product> filteredProducts = new List<Product>();
            var result = products.Where(p => p.UnitPrice > 5000 && p.UnitInStock > 3).ToList();
            foreach (var product in products)
            {
                if (product.UnitPrice > 5000 && product.UnitInStock > 3)
                {
                    Console.WriteLine(product.ProductName);
                }
            }
            return filteredProducts;
        }
        static List<Product> GetProductsLinq(List<Product> products)
        {
            return products.Where(p => p.UnitPrice > 5000 && p.UnitInStock > 3).ToList();
        }
    }

    class ProductDto
    {
        public int ProductId { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }

    }

    class Product
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitInStock { get; set; }

    }
    class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
