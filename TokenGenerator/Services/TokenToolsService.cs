using System;
using System.Linq;
using TokenGenerator.Interfaces;

namespace TokenGenerator.Services
{
    public class TokenToolsService : ITokenToolsService
    {

        public TokenToolsService()
        {
        }
        public long GenerateToken(long cardNumber, int cvv, DateTime dateTime)
        {
            // Rotates the last 4 disgits of the card using the cvv
            int[] keys = RotateArray(cardNumber.ToString().TakeLast(4).Select(i => int.Parse(i.ToString())).ToArray(), cvv);

            // Generates a token based on the array after the rotation and the current date and hour
            // The last multiplication is to increase the size of the token
            // The 13 is so that the milliseconds is not 0
            long token = (((((dateTime.Date.Day * keys[0]) * keys[1] + dateTime.Date.Month) * keys[2] + dateTime.Date.Year) * keys[3] + dateTime.Hour * dateTime.Minute * (dateTime.Millisecond + 13)) * (long)9369319) ;

            // Rotates the previous generated token by the sum of cvv digits
            token = long.Parse(
                string.Join(
                    "", 
                    RotateArray(
                        token.ToString().ToArray().Select(
                                i => int.Parse(i.ToString())
                            ).ToArray(), 
                        cvv.ToString().ToArray().Aggregate<char, int, int>(
                                0, 
                                (a, b) => b, 
                                b => b
                            )
                        )
                    )
                );
            return token;
        }

        public int[] RotateArray(int[] array, int rotation)
        {
            if (rotation % array.Length == 0)
            {
                return array;
            }

            int[] tempArray = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                if (i + rotation >= array.Length)
                {
                    tempArray[(rotation + i) % array.Length] = array[i];
                }
                else
                {
                    tempArray[i + rotation] = array[i];
                }
            }

            return tempArray;
        }
    }
}
