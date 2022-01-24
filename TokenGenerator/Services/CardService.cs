using System.Threading.Tasks;
using TokenGenerator.DAL.Contracts;
using TokenGenerator.Models;
using TokenGenerator.Services.Contracts;

namespace TokenGenerator.Services
{
    public class CardService : ICardService
    {
        private readonly ICardDao _dao;

        public CardService(ICardDao cardDao)
        {
            _dao = cardDao;
        }

        public async Task<(Card Card, Token Token)> CreateAsync(Card card)
        {
            (Card, Token) cardAndToken = await _dao.CreateAndGenerateTokenAsync(card);
            if (await _dao.SaveChangesAsync())
            {
                return cardAndToken;
            }
            return (null, null);
        }

        public async Task<Card> GetByIdAsync(int cardId)
        {
            return await _dao.GetByIdAsync(cardId);
        }


    }
}