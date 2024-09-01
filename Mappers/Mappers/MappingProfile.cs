//using BusinessDomain.DTOs;
//using AutoMapper;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Infrastructure.Entities;
////using Presentation.ViewModels;
////using Microsoft.AspNetCore.Mvc;

//namespace Runtime.Mappers
//{
//    public class MappingProfile : Profile
//    {
//        public MappingProfile()
//        {
//            CreateMap<User, UserDTO>()
//                .ReverseMap();
//            CreateMap<Product, ProductDTO>()
//                .ReverseMap();
//            CreateMap<OrderItem, OrderItemDTO>()
//                .ReverseMap();
//            CreateMap<Order, OrderDTO>()
//                // src: OrderItem[] -> dest: OrderItemDTO[]
//                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
//                .ReverseMap();

//            //CreateMap<UserDTO, UserViewModel>()
//            //    .ReverseMap();
//            //CreateMap<ProductDTO, ProductViewModel>()
//            //    .ReverseMap();
//            //CreateMap<OrderItemDTO, OrderItemViewModel>()
//            //    .ReverseMap();
//            //CreateMap<OrderDTO, OrderViewModel>()
//            //    .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
//            //    .ReverseMap();
//        }
//    }
//}
