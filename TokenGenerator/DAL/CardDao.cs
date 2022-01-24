using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TokenGenerator.DAL.Contracts;
using TokenGenerator.Interfaces;
using TokenGenerator.Models;

namespace TokenGenerator.DAL
{
    public class CardDao : ICardDao
    {
        private readonly ApplicationDbContext _context;
        private ITokenToolsService _tokenToolsService;

        public CardDao(ApplicationDbContext context, ITokenToolsService tokenToolsService)
        {
            _context = context;
            _tokenToolsService = tokenToolsService;
        }

        public async Task<(Card, Token)> CreateAndGenerateTokenAsync(Card card)
        {
            Card storedCard = await _context.Cards.FirstOrDefaultAsync(dbcard => dbcard.Number == card.Number);
            DateTime dateTime = DateTime.UtcNow;
            if (storedCard == null)
            {
                Card createdCard = (await _context.Cards.AddAsync(card)).Entity;
                storedCard = createdCard;
            }
            Token token = new Token(_tokenToolsService.GenerateToken(card.Number, card.CVV, dateTime)) { RegistrationDate = dateTime };
            Token createdToken = (await _context.Tokens.AddAsync(token)).Entity;

            return (storedCard, createdToken);
        }

        public async Task<Card> GetByIdAsync(int cardId)
        {
            return await _context.Cards.FirstOrDefaultAsync(card => card.Id == cardId);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }


    }
}
