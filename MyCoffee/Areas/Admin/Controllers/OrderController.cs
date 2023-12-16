using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCoffee.BL.IRepository;
using MyCoffee.BL.Models;
using MyCoffee.DAL.Database;
using MyCoffee.DAL.Entites;

namespace MyCoffee.Areas.Admin.Controllers
{   [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderRep rep;
        private readonly DBContext db;

        public OrderController(IOrderRep rep,DBContext db)
        {
            this.rep = rep;
            this.db = db;
        }
        public IActionResult Index()
        {
            var data = db.Order.Join(
     db.Identity,
     t1 => t1.UserId,
     t2 => t2.Id,
     (t1, t2) => new OrderVM
     {
         id = t1.id,
         UserName = t2.UserName,
         PhoneNumber = t1.PhoneNumber,
         TotalPrice = t1.TotalPrice,
         CreatedTime = t1.CreatedTime,
         ShippingDate = t1.ShippingDate,
            // Add more properties as needed
        }
 );

            return View(data.ToList());
        }
        //public async Task<IActionResult> Details(int id)

        //{
        //    var data=await db.Order.FindAsync(id);
        //    var details = db.OrderDetails.Where(o => o.OrderId == data.id).Select(o => o);

        //    IEnumerable<Product> product = new List<Product>();
        //    foreach (var item in details)
        //    {


        //        product = db.products
        //          .Where(ur => ur.Id == item.Id)
        //          .Join(db.OrderDetails, ur => ur.Id, r => r.ProductId, (ur, r) => r.Product);



        //    }

        //    var model = new OrderDetailsVM
        //    {
        //        OrderId = data.id,
        //        PhoneNumber = data.PhoneNumber,
        //        TotalPrice = data.TotalPrice,
        //        ShippingDate = data.ShippingDate,
        //        OrderDate = data.OrderDate,
        //        identity = db.Identity.Where(u => u.Id == data.UserId).Select(u => u),
        //        OrderDetails = details,


        //        Product= product
        //    };


        //    //var result = from t1 in db.Order
        //    //             where t1.id==id
        //    //           //  join t2 in db.OrderDetails on t1.id equals t2.OrderId
        //    //           //  join t3 in db.products on t2.ProductId equals t3.Id

        //    //             select new OrderDetailsVM
        //    //             {   OrderId = t1.id,
        //    //                 //Address=t4.Address,
        //    //                // City=t4.City,
        //    //                // DetailsPrice=t2.Price,
        //    //                // UserPhoto=t4.Photo,
        //    //                // UserName=t4.UserName,
        //    //                 PhoneNumber=t1.PhoneNumber,
        //    //                 TotalPrice=t1.TotalPrice,
        //    //                // Name=t3.Name,
        //    //                 ShippingDate=t1.ShippingDate,
        //    //                // ProductPrice=t3.Price,
        //    //                // ProductPhoto=t3.Photo,
        //    //                 OrderDate=t1.OrderDate,
        //    //               //  Count=t2.Count,



        //    //             };

        //    return View(model);
        //}
        //public async Task<IActionResult> Details(int id)
        //{
        //    var order = await db.Order.FindAsync(id);
        //    var user = db.Identity.FirstOrDefault(u => u.Id == order.UserId);

        //    var countorder = db.Order.Where(o => o.UserId == user.Id).Count();
        //    if (order == null)
        //    {
        //        return NotFound(); // Handle the case where the order with the specified id is not found
        //    }

        //    var orderDetails = db.OrderDetails
        //        .Where(o => o.OrderId == order.id)
        //        .ToList(); // Materialize the query to avoid multiple enumeration

        //    var productIds = orderDetails.Select(od => od.ProductId).ToList();
        //    var products = db.products
        //        .Where(p => productIds.Contains(p.Id))
        //        .ToList(); // Materialize the query to avoid multiple enumeration

        //    var productQuantities = new Dictionary<int, int>();

        //    foreach (var product in products)
        //    {
        //        var quantity = orderDetails
        //            .Where(od => od.ProductId == product.Id)
        //            .Sum(od => od.Count);

        //        productQuantities.Add(product.Id, quantity);
        //    }

        //    var model = new OrderDetailsVM
        //    {
        //        OrderId = order.id,
        //        PhoneNumber = order.PhoneNumber,
        //        TotalPrice = order.TotalPrice,
        //        ShippingDate = order.ShippingDate,
        //        OrderDate = order.OrderDate,
        //        identity = user,
        //        OrderDetails = orderDetails,
        //        Product = products,
        //        OrderCount = countorder,
        //        ProductQuantities = productQuantities

        //    };

        //    return View(model);
        //}
        public async Task<IActionResult> Details(int id)
        {
            var order = await db.Order
                .Include(o => o.orderdetail)
                .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(o => o.id == id);

            if (order == null)
            {
                return NotFound();
            }
           
            var user = db.Identity.FirstOrDefault(u => u.Id == order.UserId);

            var productQuantities = order.orderdetail
                .GroupBy(od => od.ProductId)
                .ToDictionary(g => g.Key, g => g.Sum(od => od.Count));

            var model = new OrderDetailsVM
            {
                OrderId = order.id,
                PhoneNumber = order.PhoneNumber,
                TotalPrice = order.TotalPrice,
                ShippingDate = order.ShippingDate,
                OrderDate = order.OrderDate,
                identity = user,
                OrderDetails = order.orderdetail.ToList(),
                Product = order.orderdetail.Select(od => od.Product).ToList(),
                OrderCount = db.Order.Count(o => o.UserId == user.Id),
                ProductQuantities = productQuantities
            };

            return View(model);
        }
    }
}
