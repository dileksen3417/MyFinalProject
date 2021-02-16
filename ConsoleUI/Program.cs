using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    //SOLID prensiplerinin O kısmını yaptk
    //Open Closed Principle
    //Sistemine yeni bir özellik ekliyorsan, hiçbir koda dokunmamalısın.
    class Program
    {
        static void Main(string[] args)
        {
            ProductTest();
            //CategoryTest();

             

        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var category in categoryManager.GetAll())
            {
                Console.WriteLine(category.CategoryName);
            }
        }

        private static void ProductTest()
        {
            ProductManager productManager = new ProductManager(new EfProductDal());

            Console.WriteLine("Tüm Product adlarının listesini döner.....");
            foreach (var product in productManager.GetAll())
            {
                Console.WriteLine(product.ProductName);
            }

            Console.WriteLine("\n\nCategoryid=2 olan ürünlerin adını liste tipinde döner.....");
            foreach (var product in productManager.GetAllByCategoryId(2))
            {
                Console.WriteLine(product.ProductName);
            }

            Console.WriteLine("\n\nUnitPrice'ı min 50, max 100 TL arası olan ürünlerin adını döner....");
            foreach (var product in productManager.GetByUnitPrice(50, 100))
            {
                Console.WriteLine(product.ProductName);
            }

            Console.WriteLine("\n\nProductName ve ait olduğu CategoryName listesini döner....");
            foreach (var product in productManager.GetProductDetails())
            {
                Console.WriteLine("{0} / {1}",product.ProductName,product.CategoryName);
            }
        }
    }
}
