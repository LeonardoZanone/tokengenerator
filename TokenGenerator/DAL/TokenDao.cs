using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TokenGenerator.DAL.Contracts;
using TokenGenerator.Models;

namespace TokenGenerator.DAL
{
    public class TokenDao : ITokenDao
    {
        private readonly ApplicationDbContext _context;

        public TokenDao(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Token> CreateAsync(Token token)
        {
            return (await _context.AddAsync(token)).Entity;
        }

        public Task<Token> GetByContentAsync(long token)
        {
            return _context.Tokens.FirstOrDefaultAsync(tk => tk.Content == token);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
