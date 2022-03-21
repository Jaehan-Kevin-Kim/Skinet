using System;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;

namespace API.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string> // IValueResolver<값을 가져 올 entity, 값을 받을 entity, 넣어줄 type>
    {
        private readonly IConfiguration _config;
        public ProductUrlResolver(IConfiguration config) // IConfiguration 을 가져올때 Automapper 도 있으 Microsoft 가져올 수 있게 주의하기!!!
        {
            _config = config;
        }
        // 위 IConfiguration은 appsettings에 저장된 값을 가져오기 위해 사용 되는 듯

        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _config["ApiUrl"] + source.PictureUrl; //_config[ ] 괄호 안에 들어가는 값이 appsettings.Development.json에서 localhost를 기재헀던 value의 key임. (따라서 typo가 나지 않는것이 아주 중요 함)
            }

            return null;  // If string is empty

        }
    }
}