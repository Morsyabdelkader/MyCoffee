using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoffee.BL.Models
{
    public class RegistrationVM
    {   
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        [ValidateNever]
        public string Photo { get; set; }
       
        public IFormFile? PhotoUrl { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool terms { get; set; }



    }
}
