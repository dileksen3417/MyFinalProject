using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;
        public InMemoryProductDal()
        {
            //oracle, sqlserver,postresql,mongodb gibi yerlerden gelmiş gibi simüle ediyorum.
            _products = new List<Product> {
                new Product{ProductId=1, CategoryId=1, ProductName="Bardak", UnitPrice=15, UnitsInStock=15 },
                new Product{ProductId=2, CategoryId=1, ProductName="Kamera", UnitPrice=500, UnitsInStock=3 },
                new Product{ProductId=3, CategoryId=2, ProductName="Telefon", UnitPrice=1500, UnitsInStock=2 },
                new Product{ProductId=4, CategoryId=2, ProductName="Klavye", UnitPrice=150, UnitsInStock=65 },
                new Product{ProductId=5, CategoryId=2, ProductName="Fare", UnitPrice=85, UnitsInStock=1 },

            };
        }

        public List<Product> GetAll()
        {
            return _products; //bana liste döndür diyor o yüzden veritabanımı oldugu gibi gönderiyorum.
        }
        public List<Product> GetAllByCategory(int categoryId)
        {
            //sqldeki select gibi yazmam lazımi bunun için LINQ yapısının Where methodu var. 
            //p=> için yazdığımız kurala uyan tüm dataları getirir.
            //To.List ile listeye atarız.
            return _products.Where(p => p.CategoryId == categoryId).ToList();
        }
        public void Add(Product product)
        {
            _products.Add(product); //veritabanım _product olsun, listeye ekleme işini Add ile yapıyordum o yüzden byle yazdım.
        }

        public void Delete(Product product)
        {
            ////_products.Remove(product); böyle dersem asla silmez çünkü ben burada referansını yollamıyorum aynı id bile yollasam aynı bellege işaret etmiyor. Önemli olan adresini göstermek.
            ////yapmamız gereken buraya gelen productın idsini alıp bellekte bununla eşleşen idli ref yerini bulup onu silmektir.
            ////bunun için Linq yapısı var bunun olmadığını düşünelim listeyi tek tek gezmemiz lazımdı şu şekilde bulup ref nosunu alırdık;
            //Product productToDelete = null;
            //foreach (var p in _products)
            //{
            //    if (product.ProductId==p.ProductId)
            //    {
            //        productToDelete = p;
            //    }
            //}
            //_products.Remove(productToDelete);

            //*************

            // LINQ yapısı ile yapalım kolay şekilde: LINQ=Language Integrated Query: linq ile sql gibi dataları filtreleyebiliyoruz
            // SingleOrDefault=bizim için listeyi dolasır, tek bir eleman bulmaya yarar. 
            // p=> lambda işareti deriz işte bu foreachteki foreach (var p in _products) 'e karşılık gelir yani listeyi gez deriz

            Product productToDelete = _products.SingleOrDefault(p=>p.ProductId==product.ProductId); //her bir p için , prodcut id eşit ise bul al demek
            _products.Remove(productToDelete);
        }

        public void Update(Product product)
        {
            //Gönderdiğim ürün idsine sahip olan ürünü verdiğim listeden bul
            Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            //ref nosunu getirdiğim için zaten direkt bellegi güncelleyebiliyorum:
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }
    }
}
