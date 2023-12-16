using MyCoffee.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoffee.BL.IRepository
{
    public interface ICategoryRep
    {
        public IEnumerable<Category> GetAll();
        public Category GetById(int id);

        public void Create(Category model);
        public void Edite(Category model);
        public void Delete(Category model);
        public Category GetproductById(int id);

    }
}
