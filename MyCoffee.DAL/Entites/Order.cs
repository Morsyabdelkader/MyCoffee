using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoffee.DAL.Entites
{   [Table("order")]
    public class Order
    {
        public Order()
        {
            orderdetail = new List<OrderDetails>();
        }
        public int id { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedTime { get; set; }= DateTime.Now;
        public bool IsDeleted { get; set;}=false;

        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime ShippingDate { get; set; }

        public Double TotalPrice { get; set; }


        public string PhoneNumber { get; set; }




        [ForeignKey("UserId")]
        public Identity identitie { get; set; }

        public List<OrderDetails> orderdetail { get; set; }



    }
}
