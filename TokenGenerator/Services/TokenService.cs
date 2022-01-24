using System;
using System.Threading.Tasks;
using TokenGenerator.DAL.Contracts;
using TokenGenerator.Interfaces;
using TokenGenerator.Models;
using TokenGenerator.Services.Contracts;

namespace TokenGenerator.Services
{
    public class TokenService : ITokenService
    {
        private readonly ITokenDao _dao;
        private readonly ITokenToolsService _tokenToolsService;
        private readonly ICardService _cardService;

        public TokenService(ITokenDao tokenDao, ITokenToolsService tokenToolsService, ICardService cardService)
        {
            _dao = tokenDao;
            _tokenToolsService = tokenToolsService;
            _cardService = cardService;
        }

        public async Task<bool> ValidateAsync(Token token, int cardId, int costumerId, int cvv)
        {
            Token savedToken = await _dao.GetByContentAsync(token.Content);

            if(savedToken == null)
            {
                return false;
            }

            if(DateTime.UtcNow.Subtract(savedToken.RegistrationDate).TotalMinutes > 30)
            {
                return false;
            }

            Card card = await _cardService.GetByIdAsync(cardId);
            if (card == null || card.CostumerId != costumerId)
            {
                return false;
            }

            Console.WriteLine(card.Number);
            return _tokenToolsService.GenerateToken(card.Number, cvv, savedToken.RegistrationDate) == token.Content;

        }
    }
}
