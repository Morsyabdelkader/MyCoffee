using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MyCoffee.DAL.Entites;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MyCoffee.Models
{
    public class ProductVM
    {   [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Product Name")]
        
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter Product Description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Enter Product Price")]
        public int Price { get; set; }
        [ValidateNever]
        public string Photo { get; set; }
        [Required(ErrorMessage = "Please Upload Photo")]
        public IFormFile? PhotoUrl { get; set; }
        public int Quantity { get; set; } = 1;
        [ValidateNever]
        public bool IsActive { get; set; } = true;
        public int SelectedCategoryId { get; set; }
        public DateTime Publish_Date { get; set; }= DateTime.Now;
        [Required(ErrorMessage = "Choose Category")]
        public int CategoryId { get; set; }
        [ValidateNever]
        [JsonIgnore]
        public Category category { get; set; }

    }
}
