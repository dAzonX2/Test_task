using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace Test_task
{
    internal class Program
    {
        private static List<string> FilesFound { get; } = new List<string>();
        private const string rusWord = @"\s[а-яА-Я]+";                             //Ключевое слово
        string allRusWords = "";

        private static void DirSearch(string sDir)                              //Рекурсивная функция поиска
        {
            try
            {
                foreach (var directory in Directory.GetDirectories(sDir))       //Поиск поддиректорий
                {
                    foreach (var filename in Directory.GetFiles(directory))     //Поиск файлов
                    {
                        string allRusWords = "";
                        foreach (string line in System.IO.File.ReadLines(filename))     //Построчное чтение файла
                        {
                            if (Regex.IsMatch(line, rusWord, RegexOptions.IgnoreCase))  //Поиск русских слов с использованием регулярки
                            {
                                Regex regex = new Regex(rusWord);
                                MatchCollection matches = regex.Matches(line);
                                foreach (Match match in matches)
                                    allRusWords += match.Value;
                                FilesFound.Add(filename + " " + allRusWords);
                            }
                        }
                    }

                    DirSearch(directory);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к директории: ");
            string s_directory = Console.ReadLine();
            DirSearch(@s_directory);                                    //Директория, в которой осуществляетя поиск  
            Console.WriteLine("Файлы, содержащие русские слова: ");
            Console.WriteLine();

            foreach (var file in FilesFound)
            {
                Console.WriteLine(file);
            }

        }
    }
}