using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : IProductDal
    {
        public void Add(Product entity)
        {
            //IDispossable pattern implementation of c#
            using (NorthwindContext context = new NorthwindContext())//using bittiği anda garbage collecter gelip bunları temzlesin diye kullanılan bir yöntem.
            {
                var addedEntity = context.Entry(entity); //git bana gelen entity'i benim veri kaynagımla eşleştir
                addedEntity.State = EntityState.Added; //bu veriyi eklemek istiyorum diye set ettim
                context.SaveChanges(); // kaydet yani set ettiğim seyi yap, bunları ekle
            }
        }

        public void Delete(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
        public void Update(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public Product Get(Expression<Func<Product, bool>> filter)//tek bir data gönderecek
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                return context.Set<Product>().SingleOrDefault(filter); //linqdaki SingleOrDef methodu tek bir değer döndürürdü
            }
            
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)//liste döndürecek
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                //ternary operatörünü öğrenmiştik şimdi şunu diyeceğiz; filtre verdiyse datayı filtreleyip getir,
                //filtre verilmemişse tüm datayı getir.
                //filtre null ise ilk kısım "?..." çalışır, null deil ise ikinci kısım " :..." calısır
                return filter == null 
                    ? context.Set<Product>().ToList() 
                    : context.Set<Product>().Where(filter).ToList();
                //context.Set<Product>() : git product tablosuna yerleş , ToList() : hepsini al liste olarak bana yolla.
                //.Where(filter).ToList() : filtreye göre ara ve liste olarak gönder. filtre kısmına p=>.. kodu gelecek zaten.
            }
        }

    }
}
