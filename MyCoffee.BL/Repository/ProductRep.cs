using Microsoft.EntityFrameworkCore;
using MyCoffee.BL.IRepository;
using MyCoffee.DAL.Database;
using MyCoffee.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyCoffee.BL.Repository
{
    public class ProductRep : IProductRep
    {
        private readonly DBContext db;

        public ProductRep(DBContext db)
        {
            this.db = db;
        }

        public IEnumerable<Product> Advancedsearch(string name, string discription, int minprice, int maxprice,string cs)
        {
            if (cs == "on") {
                var data = db.products.Where(x => x.Name.Equals(name) || x.Description.Equals(discription) || x.Price >= minprice && x.Price <= maxprice).Select(x => x);
                //var data = db.products.Where(x => string.Equals(x.Name, name, StringComparison.Ordinal))
                //    .Select(x => x).ToList();

                return data;
            } else { 
            var data=db.products.Where(x => x.Name == name ||x.Description==discription ||x.Price>=minprice&&x.Price<=maxprice).Select(x=>x);
            return data;
            }
        }

        public Product create(Product model)
        {
             db.products.Add(model);
            db.SaveChanges();
            return model;
        }

        public Product Delete(Product model)
        {
            db.products.Remove(model);
            db.SaveChanges();
            return model;
        }

        public Product Edite(Product model)
        {
           db.Entry(model).State=EntityState.Modified;
            db.SaveChanges();
            return model;
        }

        public IEnumerable<Product> Get()
        {
            var data = db.products.Include("category").Select(a => a);
            return data;
        }

        public IEnumerable<Product> GetByCatId(int id)
        {
            var data = db.products.Where(a => a.CategoryId == id).Select(a => a);
            return data;
        }

        public Product GetById(int id)
        {
            var data = db.products.Where(a => a.Id == id).Include("category").FirstOrDefault();
            return data;
        }

        public IEnumerable<Product> GetByName(string name)
        {
            var data = db.products.Where(a=>a.Name==name).Select(a => a);
            return data;
        }
    }
}
