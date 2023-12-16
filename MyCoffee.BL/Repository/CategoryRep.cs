using AutoMapper;
using MyCoffee.BL.IRepository;
using MyCoffee.DAL.Database;
using MyCoffee.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace MyCoffee.BL.Repository
{
    public class CategoryRep : ICategoryRep
    {
        private readonly DBContext db;

        public CategoryRep(DBContext db)
        {
            this.db = db;
        }
        public void Create(Category model)
        {
           db.categores.Add(model);
            db.SaveChanges();
        }

        public void Delete(Category model)
        {
            db.categores.Remove(model);
            db.SaveChanges();

        }

        public void Edite(Category model)
        {
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();

        }

        public IEnumerable<Category> GetAll()
        {
          var data=  db.categores.Select(a=>a);
            return data;
        }

        public Category GetById(int id)
        {
            var data = db.categores.Where(a => a.Id == id).FirstOrDefault();
            return data;
        }
        public Category GetproductById(int id)
        {
            var data = db.categores.Where(a => a.Id==id).Include(a=>a.product).FirstOrDefault();
            return data;
        }
    }
}
