using System;

namespace SyntaxAnalyser.CoreStaff 
{
    /// <summary>
    /// Список правил остовной грамматики
    /// </summary>
    public class RulesMatrix 
    {
        /// <summary>
        /// Контейнер, хранящий правила грамматики.
        /// </summary>
        private string[][] rules = new string[10][];

        public RulesMatrix() 
        {
            rules[0] = new string[] { "!N", ";" };
            rules[1] = new string[] { "for", "(", "!N", ")", "do", "!N" };
            rules[2] = new string[] { "a", ":=", "a" };
            rules[3] = new string[] { "!N", ";", "!N", ";", "!N"};
            rules[4] = new string[] { ";", "!N", ";", "!N"};
            rules[5] = new string[] { "!N", ";", "!N", ";"};
            rules[6] = new string[] { ";", "!N", ";" };
            rules[7] = new string[] { "a", "<", "a"};
            rules[8] = new string[] { "a", ">", "a"};
            rules[9] = new string[] { "a", "=", "a"};
        }

        /// <summary>
        /// Возвращает количество правил в контейнере правил.
        /// </summary>
        /// <value></value>
        public int Count { get {return rules.Length;} }

        public RulesMatrix(string[][] rules) 
        {
            this.rules = rules;
        }

        /// <summary>
        /// Метод определяет, содержится ли переданное правило в списке правил.
        /// </summary>
        /// <param name="rule">Правило, существование которого нужно проверить.</param>
        /// <returns></returns>
        public bool IsContains(string[] rule) 
        {
            for (int rulesIdx = 0; rulesIdx < rules.Length; rulesIdx++) 
            {
                string[] checkingRule = rules[rulesIdx];
                if (rule.Length != checkingRule.Length) continue;

                int symbIdx = 0;
                while (checkingRule[symbIdx].Equals(rule[symbIdx])) 
                {
                    symbIdx += 1;
                    if (symbIdx == checkingRule.Length) break;
                }

                if (symbIdx == rule.Length) return true;
            }

            return false;
        }

        /// <summary>
        /// Возвращает, под каким номером хранится переданное правило.
        /// </summary>
        /// <param name="rule"></param>
        /// <returns>Возвращает номер правила в контейнере, или -1, если такого правила не существует.</returns>
        public int GetNumberOfRule(string[] rule) 
        {
            for (int rulesIdx = 0; rulesIdx < rules.Length; rulesIdx++) 
            {
                string[] checkingRule = rules[rulesIdx];
                if (rule.Length != checkingRule.Length) continue;

                int symbIdx = 0;
                while (checkingRule[symbIdx].Equals(rule[symbIdx])) 
                {
                    symbIdx += 1;
                    if (symbIdx == checkingRule.Length) break;
                }

                if (symbIdx == rule.Length) return rulesIdx + 1;
            }

            return -1;
        }

        public string[] GetRuleByNumber(int number) 
        {
            return GetRuleByIndex(number - 1);
        }

        public string[] GetRuleByIndex(int index) 
        {
            string[] rule = new string[this.rules[index].Length];
            Array.Copy(rules[index], rule, rule.Length);
            return rule;
        }
    }
}