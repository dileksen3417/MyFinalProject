using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    //Generic Constraint( Generic Kısıt) Buraya gelebilecek T değerlerini kısıtlamak istiyorum.
    //T:class   :  sadece class yani referans tip verilebilsin
    // ,IEntity : yani sadece IEntity olan veya IEntity implemente eden bir nesne buraya gönderilebilsin.
    // new()    : newlenebilir olmalı. Burda çakallık yapıyoruz çünkü IEntity bir interface oldugu için newlenemezdi. Bunu yazınca artık (Customer) diye IEntityden newlenen bir nesne yollayabilirim.
    public interface IEntityRepository<T> where T:class,IEntity,new()
    {
        List<T> GetAll(Expression<Func<T, bool>> filter = null);//filter=null yani filtre vermesen de olur, filtre vermezse butun dataları istiyor diye anlarız
        T Get(Expression<Func<T, bool>> filter);// T nesnesi döndüren bir methodum olsun yani tel bir tane data döndürsün bana
                                                //filter yazdık sadece yani filtre verilmesini mecburi tuttuk. Çünkü bu tek bir nesne döndürüyor yani filtre olmak zorunda ki bir tane nesne bulup döndüreyim.
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
