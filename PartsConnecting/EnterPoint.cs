using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

// Хэш таблица
using HashTableApp.HashTableStructure;
using HashTableApp.HashTableStructure.HashFunctions;
using HashTableApp.HashTableStructure.ReHashFunctions;

// Лексический анализатор
using LexicalScanner;

// Синтаксический анализатор
using SyntaxAnalyser;
using SyntaxAnalyser.CoreStaff;

namespace PartsConnecting
{
    class EnterPoint
    {
        private HashTableForString hashTable;

        static void Main(string[] args)
        {
            Console.WriteLine("3/4-Compiler started!");
            EnterPoint prog = new EnterPoint();

            FileInfo fileWithProg = null;
            if (args.Length > 0)
            {
                if (File.Exists(args[0]) & args[0].EndsWith(".meth")) fileWithProg = new FileInfo(args[0]);
            }
            if (fileWithProg == null) fileWithProg = prog.GetPathByAskingUser();

            prog.Start(fileWithProg);

            
        }

        public void Start(FileInfo file)
        {   
            // Считывание всего файла
            string allFile = this.ReadToEnd(file);
            
            // Создание хэш-таблицы
            int fileLen = Int32.MaxValue;
            if (file.Length < Int32.MaxValue) fileLen = (int)file.Length ;
            hashTable = new HashTableForString(
                new SimpleAdaptStringHF(fileLen),
                new OnRandomBasedRHF(fileLen)
                );
            
            // Лексический анализ
            LexemScanner lexScan = new LexemScanner(allFile);
            lexScan.NotifyAboutIdentificator += this.OnAddIdentificator;
            List<LexemDataCell>lexTable = lexScan.DoAnalysis();

            // Синтаксический анализ
            SyntaxScanner synAnal = new SyntaxScanner(lexTable);
            List<OutputTreeCell> tree = synAnal.DoAnalysis();

            // Отрисовка получившегося дерева в консоль
            OutputTreeDrawer drawer = new OutputTreeDrawer(tree);
            drawer.DrawToConsole();

        }

        private void OnAddIdentificator(string identificator)
        {
            hashTable.Add(identificator);
        }

        private string ReadToEnd(FileInfo file)
        {
            StreamReader reader = new StreamReader(file.OpenRead());
            StringBuilder text = new StringBuilder();
            text.Append(reader.ReadToEnd());
            reader.Close();

            return text.ToString();
        }

        /// <summary>
        /// Просит ввести путь к файлу с кодом до тех пор, пока пользователь не введёт валидный путь.
        /// </summary>
        /// <returns></returns>
        public FileInfo GetPathByAskingUser()
        {
            Console.Write("Please, enter path to file with code:\n>> ");

            string path = string.Empty;
            int count = 0;
            int code = 0;
            while (code != 1)
            {
                path = Console.ReadLine();
                Console.WriteLine();
                if (!File.Exists(path))
                {
                    Console.Write("File by such path does not exists. Please, try again\n>> ");
                    count += 1;
                    continue;
                }

                if (!path.EndsWith(".meth"))
                {
                    Console.Write("File is not .meth format. Please, try again\n>> ");
                    count += 1;
                    continue;
                }

                code = 1;
            }

            if (count >= 3)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"Wow, only {count} tries for such trivial task, you are amazing...\n");
                Console.ResetColor();
            }

            return new FileInfo(path);
        }
    }
}
