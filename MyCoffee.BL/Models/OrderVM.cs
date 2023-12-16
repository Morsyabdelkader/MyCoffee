using MyCoffee.DAL.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoffee.BL.Models
{
    public class OrderVM
    {
        public int id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }

        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;

        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime ShippingDate { get; set; }
        public Double TotalPrice { get; set; }



        public string PhoneNumber { get; set; }




        
        public Identity identity { get; set; }

        public OrderDetails orderdetail { get; set; }
        public List<OrderDetails> orderdetails { get; set; }

    }
}
