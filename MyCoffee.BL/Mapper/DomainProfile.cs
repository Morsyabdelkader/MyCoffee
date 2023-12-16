using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MyCoffee.BL.Models;
using MyCoffee.DAL.Entites;
using MyCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoffee.BL.Mapper
{
    public class DomainProfile:Profile
    {
        public DomainProfile()
        {
            CreateMap<Product, ProductVM>();
            CreateMap<ProductVM, Product>();
            CreateMap<Category, CategoryVM>();
            CreateMap<CategoryVM, Category>();
            CreateMap<IdentityRole, RoleVM>();
            CreateMap<RoleVM, IdentityRole>();

        }
    }
}
