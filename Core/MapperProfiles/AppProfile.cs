using AutoMapper;
using Core.Dtos;
using Data.Entities;


namespace Core.MapperProfiles
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<DoctorDto, Doctor>().ReverseMap();
            CreateMap<OrderDto, Order>().ReverseMap();
            //.ForMember(x => x.Work_experience, opt => opt.MapFrom(src => src.Work_experience + "!"));
        }
    }
}
