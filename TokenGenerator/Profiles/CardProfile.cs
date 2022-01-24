using AutoMapper;
using TokenGenerator.DTL.v1;
using TokenGenerator.Models;

namespace TokenGenerator.Profiles
{
    public class CardProfile : Profile
    {
        public CardProfile()
        {
            IMappingExpression<CardPostDto, Card> costumerToCardMap = CreateMap<CardPostDto, Card>();
            costumerToCardMap.ForMember(card => card.Number, costumerConf => costumerConf.MapFrom(c => c.CardNumber));
        }
    }
}
