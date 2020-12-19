using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTableApp.HashTableStructure.ReHashFunctions
{
    /// <summary>
    /// Класс представляет собой рехэширование через умножение на некоторый шаг.
    /// </summary>
    public class OnMultiplicationBasedRHF : IRehashFunction
    {
        /// <summary>
        /// Шаг для рехэширования. Число, на которое умножается хэш-функция для получения другого числа.
        /// </summary>
        private int stepValue;

        /// <summary>
        /// Шаг для рехэширования. Число, на которое умножается хэш-функция для получения другого числа.
        /// </summary>
        public int StepValue { get; }

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
        /// <exception cref="ArgumentOutOfRangeException">Выбрасывается в случае, если переданный параметр maxValueOfHashFunction <= 0
        /// , потому что данный параметр преставляет собой максимальное возможное значение, генерируемое хэш-функцией.
        /// А также в случае, если параметр stepValue <= 1, так как это шаг рехэширования.</exception>
        /// <param name="stepValue">Шаг рехэширования</param>
        /// <param name="maxValueOfHashFunction">Максимальное значение, которое может сгенерировать хэш-функция.</param>
        public OnMultiplicationBasedRHF (int stepValue, int maxValueOfHashFunction)
        {
            if (maxValueOfHashFunction <= 0 || stepValue <= 1)
                throw new ArgumentOutOfRangeException();

            this.stepValue = stepValue;
            this.maxValueOfHashFunction = maxValueOfHashFunction;
        }

        /// <inheritdoc/>
        public int Rehash(int hash, int lvlOfRehash)
        {
            if (lvlOfRehash == 0) return hash;
            return (hash * this.stepValue * lvlOfRehash) % this.maxValueOfHashFunction;
        }

        public override string ToString()
        {
            return "Simple mulitplication based";
        }
    }
}
