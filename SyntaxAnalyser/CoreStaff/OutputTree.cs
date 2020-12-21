using System.Collections.Generic;
using System.Text;

namespace SyntaxAnalyser.CoreStaff 
{
    /// <summary>
    /// Дерево Вывода для Синтаксического анализа.
    /// </summary>
    public class OutputTree 
    {
        //private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public OutputTree () { }

        /// <summary>
        /// Метод, используя матрицу правил и последовательность использованных правил, строит Дерево Вывода.
        /// </summary>
        /// <param name="rulesMatrix"></param>
        /// <param name="usedRulesList"></param>
        /// <returns></returns>
        public List<OutputTreeCell> GetOutputTree(RulesMatrix rulesMatrix, List<int> usedRulesList)
        { 
            List<OutputTreeCell> treeContainer = new List<OutputTreeCell>();
            
            OutputTreeCell root = new OutputTreeCell(0, SpecialSymbs.NOT_TERMINAL_SYMB);
            treeContainer.Add(root);

            //Logger.Info("Iteration 0, tree: {0}", string.Join(" || ", treeContainer.ToArray()));

            for (int ruleIdx = usedRulesList.Count - 1; ruleIdx >= 0; ruleIdx--)
            {
                int iteration = usedRulesList.Count - ruleIdx;
                //Logger.Info("Iteration: {0} || Rule #{1} || size of tree: {2}", iteration, usedRulesList[ruleIdx], treeContainer.Count);
                //Logger.Info("- Tree at start: {0}", string.Join(" ", treeContainer.ToArray()));

                string[] ruleSymbs = rulesMatrix.GetRuleByNumber(usedRulesList[ruleIdx]);

                int insertIdx = lastIndexOfVnWithoutChilds(treeContainer) + 1;
                if ((insertIdx - 1) < 0) break;

                //Logger.Info("-- Last index of VN without childs: {0}", insertIdx - 1);
                int childsLevel = treeContainer[insertIdx - 1].Level + 1;

                // Закидывание символов из текущего правила рядом с нетерминалом, который они раскрывают
                foreach (string symb in Reverse(ruleSymbs)) { treeContainer.Insert(insertIdx, new OutputTreeCell(childsLevel, symb)); }

                //Logger.Info("- Tree at end: {0}", string.Join(" ", treeContainer.ToArray()));
            }

           // Logger.Info("\nFull tree: {0}\n", string.Join(" || ", treeContainer.ToArray()));

            return treeContainer;
        }
        
        /// <summary>
        /// Метод производит переворачивание массива строк.
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public string[] Reverse(string[] array)
        {
            string[] arrayR = new string[array.Length];
            for (int idxFromEnd = array.Length - 1; idxFromEnd >= 0; idxFromEnd--) 
            {
                int idxFromStart = (array.Length - 1) - idxFromEnd;
                arrayR[idxFromStart] = array[idxFromEnd];
            }
            return arrayR;
        }

        /// <summary>
        /// Ищет в контейнере последний терминальный символ, у которого нет дочерних элементов.
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        private int lastIndexOfVnWithoutChilds(List<OutputTreeCell> container) 
        {
            if (container.Count == 0) 
            {
                //Logger.Info("-- OutputTree is empty");
                return -1;
            }
            if (container.Count == 1 & container[0].Value.Equals(SpecialSymbs.NOT_TERMINAL_SYMB)) return 0;
            if (container[container.Count - 1].Equals(SpecialSymbs.NOT_TERMINAL_SYMB)) return container.Count - 1;

            for (int i = container.Count - 2; i >= 0; i--)
            {   
                OutputTreeCell pre = container[i];
                OutputTreeCell post = container[i + 1];
                
                if (!pre.Value.Equals(SpecialSymbs.NOT_TERMINAL_SYMB)) continue;
                if (pre.Level < post.Level) continue;
                return i;     
            }

            //Logger.Info("-- All VN with child elems");
            return -1;
        }

    }

    /// <summary>
    /// Структура представляет собой один узел дерева вывода, который инкапсулирует уровень узла в дереве и значение.
    /// Уровень необходим для того, чтобы было возможно установить иерархию узлов.
    /// </summary>
    public struct OutputTreeCell 
    {
        public int Level {get; set;}
        public string Value {get; set;}

        public OutputTreeCell(int level, string value) 
        {
            this.Level = level;
            this.Value = value;
        }

        public override string ToString() 
        {
            return ("[ " + Level + " -> " + Value + " ]");
        }
    }
}

