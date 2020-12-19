using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTableApp.HashTableStructure.ReHashFunctions
{
    /// <summary>
    /// Класс представляет собой рехэширование через псевдослучайные числа.
    /// </summary>
    public class OnRandomBasedRHF : IRehashFunction
    {
        /// <summary>
        /// Массив случайных чисел-шагов. Неявно его размер равен мощности хэш-функции, а следовательно и хэш-таблице.
        /// </summary>
        private int[] pseudoNumbersArray;

        /// <summary>
        /// Предельное значение хэш-функции, которую необходимо рехэшировать. 
        /// Данное значение необходимо, чтобы не выйти за предел допустимых значений.
        /// </summary>
        private int maxValueOfHashFunction;

        /// <summary>
        /// Предельное значение хэш-функции, которую необходимо рехэшировать. 
        /// Данное значение необходимо, чтобы не выйти за предел допустимых значений.
        /// </summary>
        public int MaxValueOfHashFunction { get { return this.maxValueOfHashFunction; } }

        /// <summary>
        /// Конструктор. Инициализирует переменны и проверяет выход параметров за пределы.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Выбрасывается в случае, если переданный параметр <= 0
        /// , потому что данный параметр преставляет собой максимальное возможное значение, генерируемое хэш-функцией.</exception>
        /// <param name="maxValueOfHashFunction">Максимальное значение, которое может сгенерировать хэш-функция.</param>
        public OnRandomBasedRHF(int maxValueOfHashFunction)
        {
            if (maxValueOfHashFunction <= 0) 
                throw new ArgumentOutOfRangeException();

            this.maxValueOfHashFunction = maxValueOfHashFunction;

            pseudoNumbersArray = new int[maxValueOfHashFunction];

            int maxRandomValue = maxValueOfHashFunction / 5;

            Random r = new Random();
            for (int i = 0; i < maxValueOfHashFunction; i++)
            {
                pseudoNumbersArray[i] = r.Next(1, maxRandomValue);
            }
        }

        /// <inheritdoc/>>
        public int Rehash(int hash, int lvlOfRehash)
        {
            if (lvlOfRehash == 0) return hash;
            if (((lvlOfRehash < 0) || (lvlOfRehash >= this.maxValueOfHashFunction)) || hash < 0) 
                throw new ArgumentOutOfRangeException();

            return (hash + pseudoNumbersArray[lvlOfRehash - 1]) % this.maxValueOfHashFunction;
        }

        public override string ToString()
        {
            return "Simple on random based";
        }
    }
}
