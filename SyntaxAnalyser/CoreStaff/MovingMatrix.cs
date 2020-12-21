using System;

namespace SyntaxAnalyser.CoreStaff
{   
    /// <summary>
    /// Матрица
    /// </summary>
    public class MovingMatrix 
    {
        /// <summary>
        /// Матрица Предшествования.
        /// </summary>
        /// <value></value>
        char?[,] movings =
        {
            { null,     '=',    null,   null,   null,   null,   null,   null,   null,   null,   null },
            { '<',      null,   '=',    null,   '<',    null,   null,   null,   null,   '<',    null },
            { null,     null,   null,   '=',    null,   null,   null,   null,   null,   null,   null },
            { '<',      null,   '>',    null,   '<',    null,   null,   null,   null,   '>',    null },
            { null,     null,   '>',    null,   null,   '=',    '=',    '=',    '=',    '>',    null },
            { null,     null,   null,   null,   '=',    null,   null,   null,   null,   null,   null },
            { null,     null,   null,   null,   '=',    null,   null,   null,   null,   null,   null },
            { null,     null,   null,   null,   '=',    null,   null,   null,   null,   null,   null },
            { null,     null,   null,   null,   '=',    null,   null,   null,   null,   null,   null },
            { '<',      null,   '>',    null,   '<',    null,   null,   null,   null,   '=',    '>'  },
            { '<',      null,   null,   null,   '<',    null,   null,   null,   null,   '<',    null }
        };
        
        /// <summary>
        /// Индексы по оси ОХ матрицы - т.е. индексы столбцов
        /// </summary>
        /// <returns></returns>
        public readonly string[] keysOfColumns = {"for", "(", ")", "do", "a", ":=", "<", ">", "=", ";", SpecialSymbs.END_SYMB};

        /// <summary>
        /// Индексы по оси ОY матрицы - т.е. индексы строк
        /// </summary>
        /// <returns></returns>
        public readonly string[] keysOfRows = {"for", "(", ")", "do", "a", ":=", "<", ">", "=", ";", SpecialSymbs.START_SYMB};

        public MovingMatrix() { }
       
        /// <summary>
        /// Конструктор, позволяющий задать свою матрицу предшествования
        /// </summary>
        public MovingMatrix(char?[,]movings, string[]keysOfRows, string[]keysOfColumns) 
        {
            this.movings = new char?[movings.GetLength(0), movings.GetLength(1)];
            Array.Copy(movings, this.movings, movings.Length);

            this.keysOfRows = new string[keysOfRows.Length + 1];
            Array.Copy(keysOfRows, this.keysOfRows, keysOfRows.Length);
            this.keysOfRows[keysOfRows.Length] = SpecialSymbs.START_SYMB;

            this.keysOfColumns = new string[keysOfColumns.Length + 1];
            Array.Copy(keysOfColumns, this.keysOfColumns, keysOfColumns.Length);
            this.keysOfColumns[keysOfColumns.Length] = SpecialSymbs.END_SYMB;
        }

        /// <summary>
        /// По ключам [строка-столбец] возвращает знак на их пересечении, либо выбрасывает ArgumentException, если значение = null.
        /// </summary>
        /// <param name="rowKey"></param>
        /// <param name="columnKey"></param>
        /// <returns></returns>
        public char? GetMove(string rowKey, string columnKey) 
        {
            int indexRow = Array.IndexOf(keysOfRows, rowKey);
            int indexCol = Array.IndexOf(keysOfColumns, columnKey);
            if (indexRow == -1 || indexCol == -1) 
                throw new ArgumentException($"No such key words in Moving Matrix: {rowKey} ? {columnKey}");
            return movings[indexRow, indexCol];
        }
    }
}