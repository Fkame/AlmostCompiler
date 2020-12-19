using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

namespace HashTableApp.HashTableStructure.HashFunctions
{
    public class SimpleAdaptStringHF : IHashFunction
    {
        /// <summary>
        /// 
        /// </summary>
        private int edgeValue;

        /// <summary>
        /// 
        /// </summary>
        public int EdgeValue { get; }

        /// <summary>
        /// 
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
