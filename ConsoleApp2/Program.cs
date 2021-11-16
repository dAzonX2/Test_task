using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace Test_task
{
    internal class Program
    {
        private static List<string> FilesFound { get; } = new List<string>();
        private const string SearchText = "hello";                              //Ключевое слово

        private static void DirSearch(string sDir)                              //Рекурсивная функция поиска
        {
            try
            {
                foreach (var directory in Directory.GetDirectories(sDir))       //Поиск поддиректорий
                {
                    foreach (var filename in Directory.GetFiles(directory))     //Поиск файлов
                    {
                        using (var streamReader = new StreamReader(filename))
                        {
                            var contents = streamReader.ReadToEnd().ToLower();

                            if (contents.Contains(SearchText))                  //Поиск слова
                            {
                                FilesFound.Add(filename);
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
            DirSearch(@"C:\Users\danil\source\repos");                          //Директория, в которой осуществляетя поиск         

            Console.WriteLine("Files containing the word " + SearchText);
            Console.WriteLine();

            foreach (var file in FilesFound)
            {
                Console.WriteLine(file);
            }

        }
    }
}