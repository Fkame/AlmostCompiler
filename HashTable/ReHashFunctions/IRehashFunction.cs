using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTableApp.HashTableStructure.ReHashFunctions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRehashFunction
    {
        /// <summary>
        /// Создаёт решерированную функцию от заданного хэша.
        /// </summary>
        /// <param name="hash">Входящий хэш, который нужно рехэшировать. Представляет собой число.</param>
        /// <param name="lvlOfRehash">Указывает, который раз происходит рехэширование, т.е. это число - 
        /// количество коллизий с данным хэшем. Начинается с 0.</param>
        /// <returns>Рехэшированное число.</returns>
        int Rehash(int hash, int lvlOfRehash);
    }
}
