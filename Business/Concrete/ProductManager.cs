using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        public List<Product> GetAll()
        {
            //İş kodları neler olabilir:
            //yetkisi var mı?
            // vs vs kuralları koyarız bunlardan gecerse alttaki kod cagrılır ve liste döner.
            return _productDal.GetAll();
        }
    }
}
