using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoffee.DAL.Entites
{   [Table("Product")]
    public class Product
    {   [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }
        public string Photo { get; set; }
        public bool IsActive { get; set; }
        public DateTime Publish_Date { get; set; }= DateTime.Now;
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category category { get; set; }
        public List<OrderDetails> orderdetail { get; set; }





    }
}
