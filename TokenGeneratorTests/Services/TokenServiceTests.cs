using Moq;
using System;
using System.Threading.Tasks;
using TokenGenerator.DAL.Contracts;
using TokenGenerator.Interfaces;
using TokenGenerator.Models;
using TokenGenerator.Services;
using TokenGenerator.Services.Contracts;
using Xunit;

namespace TokenGeneratorTests.Services
{
    public class TokenServiceTests
    {
        private readonly Mock<ITokenDao> _tokenDaoMock;
        private readonly Mock<ITokenToolsService> _tokenToolsServiceMock;
        private readonly Mock<ICardService> _cardServiceMock;
        private readonly TokenService _tokenService;

        public TokenServiceTests()
        {
            _tokenDaoMock = new Mock<ITokenDao>();
            _tokenToolsServiceMock = new Mock<ITokenToolsService>();
            _cardServiceMock = new Mock<ICardService>();
            _tokenService = new TokenService(_tokenDaoMock.Object, _tokenToolsServiceMock.Object, _cardServiceMock.Object);
        }

        [Fact]
        public async void ValidateToken_ShouldReturnFalseIfTheTokenDoesNotExist()
        {
            _tokenDaoMock.Setup(dao => dao.GetByContentAsync(It.IsAny<long>())).Returns(Task.FromResult<Token>(null));

            bool token = await _tokenService.ValidateAsync(new Token(6789), 1, 1, 123);

            Assert.False(token);
        }

        [Fact]
        public async void ValidateToken_ShouldReturnFalseIfItHasPassedMoreThan30MinutesFromTokenCreation()
        {
            _tokenDaoMock.Setup(dao => dao.GetByContentAsync(It.IsAny<long>())).Returns(
                Task.FromResult(new Token() { RegistrationDate = DateTime.UtcNow.Subtract(TimeSpan.FromMinutes(30)) }
            ));

            bool token = await _tokenService.ValidateAsync(new Token(6789), 1, 1, 123);

            Assert.False(token);
        }

        [Fact]
        public async void ValidateToken_ShouldReturnFalseIfTheCardDoesNotExist()
        {
            _tokenDaoMock.Setup(dao => dao.GetByContentAsync(It.IsAny<long>())).Returns(
                Task.FromResult(new Token() { RegistrationDate = DateTime.UtcNow.Subtract(TimeSpan.FromMinutes(10)) }
            ));

            _cardServiceMock.Setup(cardDao => cardDao.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult<Card>(null));

            bool token = await _tokenService.ValidateAsync(new Token(6789), 1, 1, 123);

            Assert.False(token);
        }

        [Fact]
        public async void ValidateToken_ShouldReturnFalseIfTheCostumerIdIsInvalid()
        {
            _tokenDaoMock.Setup(dao => dao.GetByContentAsync(It.IsAny<long>())).Returns(
                Task.FromResult(new Token() { RegistrationDate = DateTime.UtcNow.Subtract(TimeSpan.FromMinutes(10)) }
            ));

            _cardServiceMock.Setup(cardDao => cardDao.GetByIdAsync(It.IsAny<int>())).Returns(
                Task.FromResult(new Card() { CostumerId = 2 }
            ));

            bool token = await _tokenService.ValidateAsync(new Token(6789), 1, 1, 123);

            Assert.False(token);
        }

        [Fact]
        public async void ValidateToken_ShouldReturnTrueIfAllInformationIsValid()
        {
            _tokenDaoMock.Setup(dao => dao.GetByContentAsync(It.IsAny<long>())).Returns(
                Task.FromResult(new Token() { RegistrationDate = DateTime.UtcNow.Subtract(TimeSpan.FromMinutes(10)) }
            ));

            _cardServiceMock.Setup(cardDao => cardDao.GetByIdAsync(It.IsAny<int>())).Returns(
                Task.FromResult(new Card() { CostumerId = 1 }
            ));

            _tokenToolsServiceMock.Setup(ttsm => ttsm.GenerateToken(It.IsAny<long>(), It.IsAny<int>(), It.IsAny<DateTime>())).Returns(6789);

            bool token = await _tokenService.ValidateAsync(new Token(6789), 1, 1, 123);

            Assert.True(token);
        }
    }
}
