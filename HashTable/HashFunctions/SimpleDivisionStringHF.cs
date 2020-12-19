using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;
using HashTableApp.HashTableStructure.Support;

namespace HashTableApp.HashTableStructure.HashFunctions
{
    /// <summary>
    /// Создаёт хэш код, который представляет собой остаток от деления на некоторую величину EdgeValue.
    /// Работает только для строк, иначе выбрасывает исключение.
    /// </summary>
    public class SimpleDivisionStringHF : IHashFunction
    {
        /// <summary>
        /// Переменная, которая представляет собой краевое значение, от которого будет нахоидтсья отстаток от деления.
        /// Можно назвать это размером хэш-фукции. Потому что это прямое указание мощности хэш-функции.
        /// </summary>
        private int edgeValue;

        /// <summary>
        /// Переменная, которая представляет собой краевое значение, от которого будет нахоидтсья отстаток от деления.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Данный хэш будет использоваться как индексы в массиве, 
        /// так что числа меньше 0 запрещены, а 0 запрещён, так как на данное число будет происходить деление.</exception>

        public int EdgeValue 
        {  
            get { return edgeValue; }
            set
            {
                if (value >= 1) 
                    edgeValue = value;
                else 
                    throw new ArgumentOutOfRangeException("May be only > 0 number value!");
            }
        }

        /// <summary>
        /// Конструктор класса. Принимает и задаёт краевое значение.
        /// </summary>
        /// <param name="edgeValue"></param>
        public SimpleDivisionStringHF (int edgeValue)
        {
            EdgeValue = edgeValue;
        }

        /// <inheritdoc/>
        public int CreateHash(String str)
        {
            BigInteger stringValue = StringOperationsSupport.CalculateStringValue(str);
            BigInteger rez = BigInteger.Remainder(stringValue, EdgeValue);
            return (int)rez;
        }

        /// <inheritdoc/>
        public int GetMaxValue()
        {
            return this.edgeValue;
        }

        /// <inheritdoc/>
        public int GetMinValue()
        {
            return 0;
        }

        public override string ToString()
        {
            return "Simple Division for string";
        }
    }
}
