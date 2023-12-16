using MyCoffee.DAL.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoffee.BL.Models
{
    public class OrderDetailsVM
    {
        public OrderDetailsVM()
        {

        }
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        public int OrderCount { get; set; }
        public Dictionary<int, int> ProductQuantities { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public IEnumerable<Product> Product { get; set; }
        public Identity identity { get; set; }
        public IEnumerable<OrderDetails> OrderDetails { get; set; }

        public int Count { get; set; }
        public double DetailsPrice { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }

        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;

        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime ShippingDate { get; set; }

        public Double TotalPrice { get; set; }


        public string PhoneNumber { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public int ProductPrice { get; set; }
        public string ProductPhoto { get; set; }
        public bool IsActive { get; set; }
        public DateTime Publish_Date { get; set; } = DateTime.Now;
         public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string UserPhoto { get; set; }



    }
}
