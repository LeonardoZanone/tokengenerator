using Moq;
using System.Threading.Tasks;
using TokenGenerator.DAL.Contracts;
using TokenGenerator.Models;
using TokenGenerator.Services;
using Xunit;

namespace TokenGeneratorTests.Services
{
    public class CardServiceTests
    {
        private readonly Mock<ICardDao> _cardDaoMock;
        private readonly CardService _cardService;

        public CardServiceTests()
        {
            _cardDaoMock = new Mock<ICardDao>();
            _cardService = new CardService(_cardDaoMock.Object);
        }

        [Fact]
        public async void CreateAsync_ShouldReturnTheCardAndTheTokenCreated()
        {
            Card card = new Card() { Id = 2 };
            Token token = new Token() { };

            _cardDaoMock.Setup(dao => dao.CreateAndGenerateTokenAsync(It.IsAny<Card>())).Returns(
                Task.FromResult((card, token)
             ));

            _cardDaoMock.Setup(dao => dao.SaveChangesAsync()).Returns(Task.FromResult(true));

            (Card Card, Token Token) result = await _cardService.CreateAsync(new Card());

            Assert.Equal(card, result.Card);
            Assert.Equal(token, result.Token);
        }

        [Fact]
        public async void CreateAsync_ShouldReturnNullIfCantSaveInTheDb()
        {
            Card card = new Card() { Id = 2 };
            Token token = new Token() { };

            _cardDaoMock.Setup(dao => dao.CreateAndGenerateTokenAsync(It.IsAny<Card>())).Returns(
                Task.FromResult((card, token)
             ));

            _cardDaoMock.Setup(dao => dao.SaveChangesAsync()).Returns(Task.FromResult(false));

            (Card Card, Token Token) result = await _cardService.CreateAsync(new Card());

            Assert.Null(result.Card);
            Assert.Null(result.Token);
        }

        [Fact]
        public async void GetByIdAsync_ShouldReturnTheRightCard()
        {
            _cardDaoMock.Setup(dao => dao.GetByIdAsync(1)).Returns(Task.FromResult(new Card() { Id = 1 }));
            Card card = await _cardService.GetByIdAsync(1);

            Assert.NotNull(card);
            Assert.Equal(1, card.Id);
        }
    }
}
