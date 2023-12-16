using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyCoffee.DAL.Entites
{   [Table("Category")]
    public class Category
    {   [Key]
        public int Id { get; set;}
        public string Name { get; set;}
       
        public ICollection<Product> product { get; set; }



    }
}
