using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoffee.BL.Models
{
    public class LoginVM
    {   [Required]
       
        
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [ValidateNever]
        [Display(Name = "ReturnUrl")]
        public string ReturnUrl { get; set; }
        public bool rememberme { get; set; }
    }
}
