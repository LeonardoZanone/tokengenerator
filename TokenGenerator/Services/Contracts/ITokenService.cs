using System.Threading.Tasks;
using TokenGenerator.Models;

namespace TokenGenerator.Services.Contracts
{
    public interface ITokenService
    {
        Task<bool> ValidateAsync(Token token, int cardNumber, int costumerId, int cvv);
    }
}
