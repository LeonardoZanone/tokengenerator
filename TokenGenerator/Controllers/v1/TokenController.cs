using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TokenGenerator.DTL.v1;
using TokenGenerator.Interfaces;
using TokenGenerator.Models;
using TokenGenerator.Services.Contracts;

namespace TokenGenerator.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _service;
        private readonly IMapper _mapper;

        public TokenController(ITokenService tokenService, IMapper mapper)
        {
            _service = tokenService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> ValidateToken([FromBody] TokenPostDto token)
        {
            return await _service.ValidateAsync(_mapper.Map<Token>(token), token.CardId, token.CostumerId, token.CVV);
        }
    }
}
