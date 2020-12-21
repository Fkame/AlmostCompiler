using System;

namespace HashTableApp.HashTableStructure.HashFunctions
{
    /// <summary>
    /// Хэш-функция. Работает по следующему принципу: заменяет слово на сумму кодов ASCII каждого символа, после этого находит остаток
    /// деления на EdgeValue (максимальное значение).
    /// </summary>
    public class SimpleAdaptStringHF : IHashFunction
    {
        /// <summary>
        /// Максимальное значение хэш-функции
        /// </summary>
        private int edgeValue;

        /// <summary>
        /// Свойство, возвращающее максимальное значение хэш-функции
        /// </summary>
        public int EdgeValue { get; }

        /// <summary>
        /// Конструктор, принимающий максимальное значение хэш-функции.
        /// </summary>
        /// <param name="edgeValue"></param>
        public SimpleAdaptStringHF(int edgeValue)
        {
            if (edgeValue <= 0) throw new ArgumentOutOfRangeException();

            this.edgeValue = edgeValue;
        }

        /// <inheritdoc/>
        public int CreateHash(string str)
        {
            int sum = 0;
            for (int i = 0; i < str.Length; i++)
            {
                sum += str[i];
            }
            return sum % this.edgeValue;
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
            return "Simple Adaptive for string";
        }
    }
}
