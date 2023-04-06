using AutoMapper;
using DomecChallange.Domain.Entities;
using DomecChallange.Dtos.ProdcutDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomecChallange.Mapper
{
    public class ProfileMapper:Profile
    {
        public ProfileMapper()
        {
            CreateMap<Product, EditProductDto>();
            CreateMap<Product, ProductDto>();
            CreateMap<EditProductDto, Product>().ForMember(x => x.Code, opt => opt.Ignore());
            CreateMap<Product, AddProductDto>().ReverseMap();
            CreateMap<EditProductDto, AddProductDto>().ReverseMap();
        }
    }
}
