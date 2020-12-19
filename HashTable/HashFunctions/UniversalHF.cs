using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

namespace HashTableApp.HashTableStructure.HashFunctions
{
    /// <summary>
    /// Самая универсальная хэш-функци. Берёт коды, получаемые от GetHashCode() метода класса Object.
    /// </summary>
   public class UniversalHF : IHashFunction
    {
        /// <inheritdoc/>
        public int CreateHash (String str)
        {
            return str.GetHashCode();
        }

        /// <inheritdoc/>
        public int GetMaxValue()
        {
            return int.MaxValue;
        }

        /// <inheritdoc/>
        public int GetMinValue()
        {
            return 0;
        }

        public override string ToString()
        {
            return "Using GetHashCode()";
        }

    }
}
