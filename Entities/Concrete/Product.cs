using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Product : IEntity
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public short UnitsInStock { get; set; } // short int'in bir küçüküğü, nortwind dbde bu alanın karsılı c#da buna karsılık geliyor.
        public decimal UnitPrice { get; set; }

    }
}
