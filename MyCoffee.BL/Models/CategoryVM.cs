using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MyCoffee.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyCoffee.BL.Models
{
    public class CategoryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [ValidateNever]
        
        public ICollection<Product> product { get; set; }
    }
}
