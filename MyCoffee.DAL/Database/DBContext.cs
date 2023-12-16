using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyCoffee.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoffee.DAL.Database
{
    public class DBContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public DBContext(DbContextOptions<DBContext>opts):base(opts)
        {
                
        }
       public DbSet<Product> products { set; get; }
        public DbSet<Category> categores { set; get; }
        public DbSet<Identity> Identity { set; get; }
        public DbSet<OrderDetails> OrderDetails { set; get; }
        public DbSet<Order> Order { set; get; }



    }
}
