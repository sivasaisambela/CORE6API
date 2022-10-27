using AutoMapper;
using MyHomeGroup_VillaApi.Models;
using MyHomeGroup_VillaApi.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amenties = MyHomeGroup_VillaApi.Models.Amenties;

namespace MyHomeGroup_VillaApi
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa, VillaDTO>();
            CreateMap<Amenties, MyHomeGroup_VillaApi.Models.Dto.Amenties>();
            CreateMap<MyHomeGroup_VillaApi.Models.Dto.Amenties, Amenties>();
            CreateMap<VillaDTO, Villa>();

            CreateMap<VillaDTO, VillaCreatedDTO>().ReverseMap();
            CreateMap<Villa, VillaUpdatedDTO>().ReverseMap();
        }
    }
}
