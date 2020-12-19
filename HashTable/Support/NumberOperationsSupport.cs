using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTableApp.HashTableStructure.Support
{
    public static class NumberOperationsSupport
    {
        /// <summary>
        /// Вычисляет длину строки. 
        /// Как это работает: https://coderoad.ru/6864991/%D0%9A%D0%B0%D0%BA-%D0%BF%D0%BE%D0%BB%D1%83%D1%87%D0%B8%D1%82%D1%8C-%D1%80%D0%B0%D0%B7%D0%BC%D0%B5%D1%80-%D1%87%D0%B8%D1%81%D0%BB%D0%B0-%D0%B2-net
        /// </summary>
        /// <param name="num">Число, длину которого нужно найти.</param>
        /// <returns>Длина числа.</returns>
        public static int GetNumberLenght(long num)
        {
            return (int)(Math.Log10(Math.Max(Math.Abs(num), 1)) + 1);
        }
    }
}
