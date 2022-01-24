using Moq;
using System;
using System.Globalization;
using TokenGenerator.Interfaces;
using TokenGenerator.Services;
using Xunit;

namespace TokenGeneratorTests.Services
{
    public class TokenServiceToolsTests
    {
        private readonly TokenToolsService _tokenService;

        public TokenServiceToolsTests()
        {
            _tokenService = new TokenToolsService();
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3 }, 3, new int[] { 1, 2, 3 })]
        [InlineData(new int[] { 1, 2, 3 }, 6, new int[] { 1, 2, 3 })]
        [InlineData(new int[] { 1, 2, 3 }, 0, new int[] { 1, 2, 3 })]
        public void RotateArray_ShouldNotRotateIfRotationIsAMultipleOfLenghtOrZero(int[] array, int rotation, int[] result)
        {
            Assert.Equal(result, _tokenService.RotateArray(array, rotation));
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3, 4 }, 3, new int[] { 2, 3, 4, 1 })]
        [InlineData(new int[] { 1, 2, 3, 4 }, 5, new int[] { 4, 1, 2, 3 })]
        [InlineData(new int[] { 1, 2, 3, 4 }, 15, new int[] { 2, 3, 4, 1 })]
        public void RotateArray_ShouldRotateTheArrayByTheGivenAmount(int[] array, int rotation, int[] result)
        {
            Assert.Equal(result, _tokenService.RotateArray(array, rotation));
        }

        [Fact]
        public void GenerateToken_ShoudGenerateATokenBasedOnTheCardNumberAndCVV()
        {
            DateTime dateTime = DateTime.Parse("01/24/2022 19:06:56", CultureInfo.InvariantCulture);
            Assert.Equal(463286505048, _tokenService.GenerateToken(123456789, 124, dateTime));
        }
    }
}
