using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext>: IEntityRepository<TEntity>
        where TEntity: class,IEntity, new()  //TEntity nesnesi için kurallarımı yazarım
        where TContext: DbContext, new() //Her classı yazamasın, sadece DbContext'ten inherit edilen classları gönderebilsin
    {
        public void Add(TEntity entity)
        {
            //IDispossable pattern implementation of c#
            using (TContext context = new TContext())//using bittiği anda garbage collecter gelip bunları temzlesin diye kullanılan bir yöntem.
            {
                var addedEntity = context.Entry(entity); //git bana gelen entity'i benim veri kaynagımla eşleştir
                addedEntity.State = EntityState.Added; //bu veriyi eklemek istiyorum diye set ettim
                context.SaveChanges(); // kaydet yani set ettiğim seyi yap, bunları ekle
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public TEntity Get(Expression<Func<TEntity, bool>> filter)//tek bir data gönderecek
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter); //linqdaki SingleOrDef methodu tek bir değer döndürürdü
            }

        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)//liste döndürecek
        {
            using (TContext context = new TContext())
            {
                //ternary operatörünü öğrenmiştik şimdi şunu diyeceğiz; filtre verdiyse datayı filtreleyip getir,
                //filtre verilmemişse tüm datayı getir.
                //filtre null ise ilk kısım "?..." çalışır, null deil ise ikinci kısım " :..." calısır
                return filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
                //context.Set<Product>() : git product tablosuna yerleş , ToList() : hepsini al liste olarak bana yolla.
                //.Where(filter).ToList() : filtreye göre ara ve liste olarak gönder. filtre kısmına p=>.. kodu gelecek zaten.
            }
        } 
    }
}
