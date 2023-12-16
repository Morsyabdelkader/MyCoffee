using Microsoft.EntityFrameworkCore;
using MyCoffee.BL.IRepository;
using MyCoffee.DAL.Database;
using MyCoffee.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoffee.BL.Repository
{
    public class OrderRep : IOrderRep
    {
        private readonly DBContext db;

        public OrderRep(DBContext db)
        {
            this.db = db;
        }
        //public IEnumerable<Order> Get()
        //{

        //    return;
        //}
    }
}
