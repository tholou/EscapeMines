using Betsson.EscapeMines.Service;
using System;
using System.IO;

namespace Betsson.EscapeMines.App
{
    public class Program
    {
        const string fileName = "gameSettings.txt";
        const string filePath = "./";

        static void Main()
        {
            try
            {
                new Program().Run();
            } 
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        public void Run()
        {
            var fileStream = new FileStream(filePath + fileName, FileMode.Open);
            var fileService = new GameBuilder(fileStream, fileName);
            fileService.LoadData();
        }
    }
}
