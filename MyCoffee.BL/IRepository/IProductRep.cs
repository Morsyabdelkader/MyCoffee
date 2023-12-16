using MyCoffee.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoffee.BL.IRepository
{
    public interface IProductRep
    {
        public IEnumerable<Product> Get();
        public IEnumerable<Product> GetByName(string name);
        public IEnumerable<Product> GetByCatId(int id);
        public IEnumerable<Product> Advancedsearch(string name ,string discription,int beetweenprice,int andprice,string cs);


        public Product GetById(int id);

        public Product create(Product model);
        public Product Edite(Product model);
        public Product Delete(Product model);

    }
}
