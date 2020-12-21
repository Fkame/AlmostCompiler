using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LexicalScanner
{
    /// <summary>
    /// Проводит лексический анализ текста файла. На выходе получается заполненная таблица лексем.
    /// </summary>
    public class LexemScanner
    {
        /// <summary>
        /// Текст файла.
        /// </summary>
        private string FullText;
        
        /// <summary>
        /// Таблица Лексем. Сюда заносятся все найденные лексемы.
        /// </summary>
        List<LexemDataCell> LexemTable = null;

        /// <summary>
        /// Список ключевых слов.
        /// </summary>
        /// <value></value>
        private string[] KeyWords = { "for", "do" };

        /// <summary>
        /// Список разделителей.
        /// </summary>
        /// <returns></returns>
        private char[] Splitters = { ';', '(', ')' };

        /// <summary>
        /// Список операций.
        /// </summary>
        /// <value></value>
        private char[] Conditions = { '>', '<', '=' };

        /// <summary>
        /// Делегат для вызова методов, подписанных на событие NotifyAboutIdentificator.
        /// </summary>
        /// <param name="identificator"></param>
        public delegate void IdentificatorHandler(string identificator);

        /// <summary>
        /// Событие вызываемое, когда среди лексем анализатор находит Идентификаторы.
        /// </summary>
        public event IdentificatorHandler NotifyAboutIdentificator;

        public LexemScanner(string fullText)
        {
            this.FullText = fullText;
        }

        /// <summary>
        /// Метод, запускающий процесс анализа лексем.
        /// </summary>
        /// <returns></returns>
        public List<LexemDataCell> DoAnalysis()
        {
            if (FullText.Equals(string.Empty))
                return null;

            LexemTable = new List<LexemDataCell>();
            int numOfStr = 1;

            for (int point = 0; point < FullText.Length; point++)
            {
                (bool isSuccess, StringBuilder value) result;

                if (FullText[point] == '\n')
                {
                    numOfStr += 1;
                    continue;
                }

                if (FullText[point] == ' ' | FullText[point] == '\t')
                {
                    continue;
                }

                result = TryIdentificator(ref point);
                if (result.isSuccess)
                {
                    point -= 1;
                    LexemType type = LexemType.Identificator;
                    if (IsKeyWord(result.value))
                    {
                        type = LexemType.Key_Word;
                    }

                    // Сообщим обработчикам, что найден идентификатор
                    NotifyAboutIdentificator?.Invoke(result.value.ToString());
                    
                    AddCell(numOfStr, result.value.ToString(), type);
                    continue;               
                }
                
                result = TryStringConstant(ref point);
                if (result.isSuccess)
                {
                    point -= 1;
                    AddCell(numOfStr, result.value.ToString(), LexemType.Char_Constant);
                    continue;
                }

                result = TryNumberconstant(ref point);
                if (result.isSuccess)
                {
                    point -= 1;
                    AddCell(numOfStr, result.value.ToString(), LexemType.Number_Constant);
                    continue;
                }

                if (FullText[point] == ':' & (point < FullText.Length - 1))
                {
                    if (FullText[point + 1] == '=')
                    {
                        AddCell(numOfStr, ":=", LexemType.Assignment_Symbol);
                        point += 1;
                        continue;
                    }
                }

                if (Splitters.Contains(FullText[point]))
                {
                    AddCell(numOfStr, FullText[point].ToString(), LexemType.Splitter);
                    continue;
                }

                if (Conditions.Contains(FullText[point]))
                {
                    AddCell(numOfStr, FullText[point].ToString(), LexemType.Condition_Symbol);
                    continue;
                }
            }

            return LexemTable;
        }

        /// <summary>
        /// Метод анализирует, является ли последующая цепочка символов идентификатором. 
        /// Если нет, то значение IsSuccess будет false, иначе true
        /// </summary>
        /// <param name="index">Индекс, с которого начинается анализ</param>
        /// <returns>Кортеж: удачно ли прошла операция, значение.</returns>
        internal (bool isSuccess, StringBuilder value) TryIdentificator(ref int index)
        {
            if (index == FullText.Length) return (false, null);

            StringBuilder container = new StringBuilder();
            char firstCh = FullText[index];
            if (firstCh != '_' & !IsEnLetter(firstCh)) return (false, null);

            bool isHaveLetters = false;
            char checkCh = FullText[index];
            while ((IsEnLetter(checkCh) | IsNumber(checkCh) | checkCh == '_'))
            {
                if (IsEnLetter(checkCh)) isHaveLetters = true;
                container.Append(checkCh);
                index += 1;
                if (index < FullText.Length)
                    checkCh = FullText[index];
                else
                    break;
            }

            if (!isHaveLetters)
            {
                container.Clear();
                return (false, null);
            }

            return (true, container);
        }
        
        /// <summary>
        /// Определяет, является ли входной символ буквой английского алфавита.
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        internal bool IsEnLetter(char ch)
        {
            return ((ch >= 'a' & ch <= 'z') | (ch >= 'A' & ch <= 'Z'));
        }

        /// <summary>
        /// Определяет, является ли входной символ цифрой.
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        internal bool IsNumber(char ch)
        {
            return ch >= '0' & ch <= '9';
        }
        
        /// <summary>
        /// Определяет, является ли входная строка ключевым словом.
        /// </summary>
        /// <param name="identificator"></param>
        /// <returns></returns>
        internal bool IsKeyWord(StringBuilder identificator)
        {
            return KeyWords.Contains(identificator.ToString());
        }

        /// <summary>
        /// Метод анализирует, является ли последующая цепочка символов символьной константой. 
        /// Если нет, то значение IsSuccess будет false, иначе true
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        internal (bool isSuccess, StringBuilder value) TryStringConstant(ref int index)
        {
            if (index == FullText.Length) return (false, null);
            if (FullText[index] != '\"') return (false, null);
            index += 1;

            StringBuilder container = new StringBuilder();
            while ((FullText[index] != '\"') & (FullText[index] != '\n'))
            {
                container.Append(FullText[index]);
                index += 1;

                if (index == FullText.Length)
                {
                    container.Clear();
                    return (false, null);
                }
            }

            if (FullText[index] != '\"')
            {
                container.Clear();
                return (false, null);
            }

            index += 1;
            return (true, container);
        }

        /// <summary>
        /// Метод анализирует, является ли последующая цепочка символов числом. 
        /// Если нет, то значение IsSuccess будет false, иначе true
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        internal (bool isSuccess, StringBuilder value) TryNumberconstant(ref int index)
        {
            if (index == FullText.Length) return (false, null);

            StringBuilder container = new StringBuilder();
            while (IsNumber(FullText[index]))
            {
                container.Append(FullText[index]);
                index += 1;

                if (index == FullText.Length)
                {
                    break;
                }
            }

            if (container.Length == 0) return (false, null);
            return (true, container);
        }

        /// <summary>
        /// Добавляет значение в таблицу лексем
        /// </summary>
        /// <param name="numOfStr">Номер строки в файле, где написано слово</param>
        /// <param name="lexem">Лексема</param>
        /// <param name="lexType">Тип лексемы. См. перечисление LexemType</param>
        internal void AddCell(int numOfStr, string lexem, LexemType lexType)
        {
            LexemTable.Add(new LexemDataCell(numOfStr, lexem, lexType));
        }
    }
    
}
