using AutoMapper;
using TokenGenerator.DTL.v1;
using TokenGenerator.Models;

namespace TokenGenerator.Profiles
{
    public class TokenProfile : Profile
    {
        public TokenProfile()
        {
            var tokenPostMap = CreateMap<TokenPostDto, Token>();
            tokenPostMap.ForMember(token => token.Content, map => map.MapFrom(dto => dto.Token));
        }
    }
}
