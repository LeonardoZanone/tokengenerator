using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TokenGenerator.DTL.v1;
using TokenGenerator.Models;
using TokenGenerator.Services.Contracts;

namespace TokenGenerator.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardService _service;
        private readonly IMapper _mapper;

        public CardController(ICardService cardService, IMapper cardMapper)
        {
            _service = cardService;
            _mapper = cardMapper;
        }

        [HttpPost]
        public async Task<ActionResult<CardPostResponseDto>> CreateCostumer([FromBody] CardPostDto card)
        {
            (Card Card, Token Token) created = await _service.CreateAsync(_mapper.Map<Card>(card));

            if (created.Token is null)
            {
                return BadRequest(ModelState);
            }

            return Ok(new CardPostResponseDto() { RegistrationDate = created.Token.RegistrationDate, Token = created.Token.Content, CardId = created.Card.Id });
        }
    }
}
