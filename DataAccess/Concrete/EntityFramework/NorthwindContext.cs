using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    //Context : db tabloları ile proje classlarını ilişkilendirmek demektir.
    class NorthwindContext:DbContext //DbContext, framework içinde yer alan bir base classtır. Yani bu classı gerçekten Context yapabilmek için DbContext classını base al deriz. 
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)//veritabanımızın baglantısını yapalım
        {
            //.UseSqlServer : yani sqlserver ile çalışacagımı belirtiyorum.
            //optionsBuilder.UseSqlServer(@"Server=175.45.2.12"); //Db'min olduğu serverın ipsini veriyorum genelde projelerde bu şekilde görürüz fakat biz localde çalıştığımız için kendi localdeki adresimizi vericez bunun için SqlServerObjectExplorer penceresinden yolunu görebilirz. 
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Northwind;Trusted_Connection=true");
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
