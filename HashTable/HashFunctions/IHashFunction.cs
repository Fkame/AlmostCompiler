using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

namespace HashTableApp.HashTableStructure.HashFunctions
{
    /// <summary>
    /// Маркировочный интерфейс, создаанный для группировки хэш-функций.
    /// </summary>
    public interface IHashFunction
    {
        /// <summary>
        /// Метод создания хэша
        /// </summary>
        /// <param name="str">Строка, которую нужно захэшировать</param>
        /// <returns></returns>
        int CreateHash(String str);

        /// <summary>
        /// Возвращает максимальное значение, которое может получить хэш-функция.
        /// </summary>
        /// <returns>Максимально возможное число, которое может сгенерировать хэш-функция</returns>
        int GetMaxValue();

        /// <summary>
        /// Возвращает минимальное значение, которое может получить хэш-функция.
        /// </summary>
        /// <returns>Минимально возможное число, которое может сгенерировать хэш-функция</returns>
        int GetMinValue();
    }
}
