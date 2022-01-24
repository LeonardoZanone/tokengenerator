using System.Threading.Tasks;
using TokenGenerator.Models;

namespace TokenGenerator.Services.Contracts
{
    public interface ICardService
    {
        Task<Card> GetByIdAsync(int costumerId);
        Task<(Card Card, Token Token)> CreateAsync(Card card);
    }
}