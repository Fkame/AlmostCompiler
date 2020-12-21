using System.Collections.Generic;
using System.Text;

namespace SyntaxAnalyser.CoreStaff 
{
    /// <summary>
    /// Класс, выполняющий красивую структурированную отрисовку дерева в консоль.
    /// </summary>
    public class OutputTreeDrawer 
    {
        /// <summary>
        /// Ссылка на контейнер с деревом вывода
        /// </summary>
        /// <value></value>
        public List<OutputTreeCell> OutputTree { get; set; }

        //private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        
        /// <summary>
        /// Параметр, хранящий количество горизонтальный черточек перед символом.
        /// </summary>
        private int amountOfSlashesBeforeSymbol = 1;

        /// <summary>
        /// Свойство, позволяющее задать количество горизонтальный черточек перед символом.
        /// </summary>
        /// <value></value>
        public int AmountOfSlashesBeforeSymbol 
        { 
            get { return amountOfSlashesBeforeSymbol; } 
            set { if (value >= 1) amountOfSlashesBeforeSymbol = value; } 
        }

        /// <summary>
        /// Параметр, хранящий количество пробелов перед символом.
        /// </summary>
        private int amountOfSpacesBeforeSymbol = 1;

        /// <summary>
        /// Свойство, позволяющее задать количество пробелов перед символом.
        /// </summary>
        /// <value></value>
        public int AmountOfSpacesBeforeSymbol 
        { 
            get { return amountOfSpacesBeforeSymbol; } 
            set { if (value >= 1) amountOfSpacesBeforeSymbol = value; } 
        }

        public OutputTreeDrawer(List<OutputTreeCell> tree)
        {
            OutputTree = tree;
        }

        /// <summary>
        /// Метод, выполняющий красивую отрисвоку Дерева вывода в консоль.
        /// </summary>
        public void DrawToConsole() 
        {
            if (OutputTree.Count == 0) throw new System.Exception("Пустое дерево - нечего рисовать!");
            if (OutputTree.Count == 1) MakeDrawing(new StringBuilder(OutputTree[0].Value));

            StringBuilder treeD = new StringBuilder();
            List<int> currentChildsAtLevels = new List<int>();

            for (int i = 0; i <= GetMaxLevel(); i++) 
                currentChildsAtLevels.Add(0);

            treeD.Append(OutputTree[0].Value).Append('\n');

            //Logger.Info("GetMaxLevel = {0}, Len of OutputTree = {1}", GetMaxLevel(), OutputTree.Count);

            int lastNodeLvl = OutputTree[0].Level;
            for (int i = 1; i < OutputTree.Count; i++)
            {
                OutputTreeCell currentNode = OutputTree[i];
                int currentLvl = currentNode.Level;
                if (lastNodeLvl < currentLvl) 
                    currentChildsAtLevels[lastNodeLvl] = FindAmountOfDirectChilds(i - 1);

                //Logger.Info("Now symb: [ {0} -> {1} ], currLvl={2}, lastLvl={3} array of direct childs: {4}", 
                    //currentLvl, currentNode.Value, currentLvl, lastNodeLvl, string.Join(" ", currentChildsAtLevels.ToArray()));

                for (int j = 0; j < currentLvl; j++) 
                {
                    if (currentChildsAtLevels[j] > 0) 
                        treeD.Append('|');
                    else treeD.Append(' ');

                    if (currentLvl - j > 1)
                        SpamSymbol(treeD, AmountOfSlashesBeforeSymbol + AmountOfSpacesBeforeSymbol, ' ');

                    if (currentNode.Level - 1 == j) 
                        ReduceChildsAmountBy1(currentChildsAtLevels, currentNode.Level);
                }

                SpamSymbol(treeD, AmountOfSlashesBeforeSymbol, '-');
                SpamSymbol(treeD, AmountOfSpacesBeforeSymbol, ' ');
                treeD.Append(currentNode.Value);
                treeD.Append('\n');

                lastNodeLvl = currentLvl;
            }
    
            MakeDrawing(treeD);
        }

        private void ReduceChildsAmountBy1(List<int> amountOnLvls, int lvlOfCurrentNode)
        {
            //Logger.Info("-- Redicing index = {0}", lvlOfCurrentNode - 1);
            if (amountOnLvls[lvlOfCurrentNode - 1] > 0) 
                amountOnLvls[lvlOfCurrentNode - 1] -= 1;
        }

        /// <summary>
        /// Непосредственно отрисовка дерева по построенному формату.
        /// </summary>
        /// <param name="treeD"></param>
        private void MakeDrawing(StringBuilder treeD) 
        {
            System.Console.WriteLine(treeD);
        }

        /// <summary>
        /// Метод, дублирующий переданный символ указанное количество раз в указанный контейнер.
        /// </summary>
        /// <param name="targerToSpam">Контейнер, в который необходимо продублировать переданный символ</param>
        /// <param name="amount">Количество раз, которое необходимо продублировать переданный символ</param>
        /// <param name="symb">Символ, который необходимо продублировать</param>
        private void SpamSymbol(StringBuilder targerToSpam, int amount, char symb) 
        {
            while (amount-- > 0) targerToSpam.Append(symb);
        }

        /// <summary>
        /// Метод находит количество прямых потомков узла.
        /// Прямые потомки, это узлы, уровень которые на 1 выше уровня интересующего узла.
        /// </summary>
        /// <param name="indexOfCalculatingCell"></param>
        /// <returns></returns>
        private int FindAmountOfDirectChilds(int indexOfCalculatingCell)
        {
            int currentLvl = OutputTree[indexOfCalculatingCell].Level;
            int findingLevels = currentLvl + 1;
            int count = 0;
            for (int i = indexOfCalculatingCell + 1; i < OutputTree.Count; i++)
            {
                if (OutputTree[i].Level < findingLevels) break;
                if (OutputTree[i].Level > findingLevels) continue;
                count += 1;
            }
            
            //Logger.Info("---- FindAmountOfDirectChilds: indexOfParent={0}, countOfDirectChildrens={1}, lvlOfParent={2}", 
                //indexOfCalculatingCell, count, currentLvl);
            return count;

        }
        
        /// <summary>
        /// Метод находит максимальный уровень в дереве.
        /// </summary>
        /// <returns></returns>
        public int GetMaxLevel() 
        {
            int maxLvl = 0;
            foreach(OutputTreeCell cell in OutputTree) 
            {
                if (cell.Level > maxLvl) maxLvl = cell.Level;
            }
            return maxLvl;
        }

        /// <summary>
        /// Метод находит сколько узлов на каком уровне находятся. Что-то вроде общей статистики по дереву.
        /// </summary>
        /// <param name="tree"></param>
        /// <returns></returns>
        public List<int> GetAmountAtEachLevel (List<OutputTreeCell> tree) 
        {
            List<int> amountAtLevels = new List<int>();
            foreach (OutputTreeCell cell in tree)
            {
                if (cell.Level > amountAtLevels.Count + 1) amountAtLevels.Add(1);
                amountAtLevels[cell.Level] += 1;
            }

            return amountAtLevels;
        }
    }
}