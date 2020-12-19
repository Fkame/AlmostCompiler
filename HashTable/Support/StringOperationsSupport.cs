using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

namespace HashTableApp.HashTableStructure.Support
{
    public static class StringOperationsSupport
    {
        /// <summary>
        /// Меняет каждую букву строки на её ASCII код.
        /// </summary>
        /// <param name="str">Входная строка, которую нужно перевести в число</param>
        /// <returns>Численное значение строки</returns>
        public static BigInteger CalculateStringValue(String str)
        {
            BigInteger stringValue = BigInteger.Zero;
            BigInteger iterator = 1;
            for (int i = str.Length - 1; i >= 0; i--)
            {
                int charToInt = str[i];
                stringValue += charToInt * iterator;
                iterator *= (int)Math.Pow(10, NumberOperationsSupport.GetNumberLenght(charToInt));
            }
            return stringValue;
        }
    }
}
