using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoffee.BL.Models
{
    public class RoleVM
    {  
        [Required(ErrorMessage ="Please Enter Role Name")]
        public string Name { get; set; }
    }
}
