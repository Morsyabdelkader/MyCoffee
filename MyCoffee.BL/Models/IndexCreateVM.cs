using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MyCoffee.DAL.Entites;
using MyCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoffee.BL.Models
{
    public class IndexCreateVM
    {
        [ValidateNever]
        public IEnumerable<ProductVM> Products { get; set; }
        public ProductVM Product { get; set; }
        [ValidateNever]
        public IEnumerable<CategoryVM> Categories { get; set; }
    }
}
