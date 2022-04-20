using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // CreateMap<Product, ProductToReturnDto>();
            CreateMap<Product, ProductToReturnDto>()
            .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
            .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
            .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>()); // ProductUrlResolver.cs에서 생성한 ProductUrlResolver가 type의 형태로 들어가짐

            // 아래 Address와 AddressDto의 경우 Property Name이 다 똑같기 때문에 위와 같이 additional 설정을 해 줄 필요가 없음
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}