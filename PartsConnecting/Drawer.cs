using System;
using System.Collections.Generic;

using HashTableApp.HashTableStructure;
using LexicalScanner;
using SyntaxAnalyser.CoreStaff;

namespace PartsConnecting
{   
    /// <summary>
    /// Статический класс для отрисовки ключевых структур в консоль.
    /// </summary>
    public static class Drawer
    {
        /// <summary>
        /// Метод для отрисовки Таблицы Лексем.
        /// </summary>
        /// <param name="lt"></param>
        public static void DrawLexTableToConsole(List<LexemDataCell> lt)
        {
            foreach(LexemDataCell cell in lt)
            {
                Console.WriteLine("| line #{0}, type=[ {1} ] --> [ {2} ]", cell.NumOfString, cell.LexType, cell.Lexem);
            }
        }

        /// <summary>
        /// Метод для отрисовки Таблицы Идетификаторов.
        /// </summary>
        /// <param name="ht"></param>
        public static void DrawIdentTableToConsole(HashTableForString ht)
        {
            string[] values = ht.GetOnlyNotNullValues();
            for (int i = 0; i < values.Length; i++)
            {
                Console.WriteLine("[ {0} ] = {1}", i, values[i]);
            }
        }

        /// <summary>
        /// Метод для отрисовки Дерева Вывода.
        /// </summary>
        /// <param name="tree"></param>
        public static void DrawOutputTreeToConsole(List<OutputTreeCell> tree)
        {
            OutputTreeDrawer drawer = new OutputTreeDrawer(tree);
            drawer.DrawToConsole();
        }
    }
}