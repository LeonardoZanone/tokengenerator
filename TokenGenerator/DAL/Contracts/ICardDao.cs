using System.Threading.Tasks;
using TokenGenerator.Models;

namespace TokenGenerator.DAL.Contracts
{
    public interface ICardDao
    {
        Task<bool> SaveChangesAsync();
        Task<Card> GetByIdAsync(int cardId);
        Task<(Card, Token)> CreateAndGenerateTokenAsync(Card card);
    }
}
