using System;

namespace TokenGenerator.Interfaces
{
    public interface ITokenToolsService
    {
        long GenerateToken(long cardNumber, int cvv, DateTime dateTime);
        int[] RotateArray(int[] array, int rotation);
    }
}
