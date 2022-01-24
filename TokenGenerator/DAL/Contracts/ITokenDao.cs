using System.Threading.Tasks;
using TokenGenerator.Models;

namespace TokenGenerator.DAL.Contracts
{
    public interface ITokenDao
    {
        Task<Token> CreateAsync(Token token);
        Task<Token> GetByContentAsync(long token);
        Task<bool> SaveChangesAsync();
    }
}
