using Betsson.EscapeMines.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Betsson.EscapeMines.UnitTest
{
    [TestClass]
    public class GameBuilderTests
    {
        private byte[] CreateStreamBytes()
        {
            string firstLine = "5 4";
            string secondLine = "1,1 1,3 3,3";
            string thirdLine = "4 2";
            string fourthLine = "0 1 N";
            string fifthLine = "M R M M M R M";
            string sixthLine = "M R M M M M R M M M";
            string seventhLine = "M R M M M M R M M";

            var memoryStream = new MemoryStream();

            var writer = new StreamWriter(memoryStream);
            writer.WriteLine(firstLine);
            writer.WriteLine(secondLine);
            writer.WriteLine(thirdLine);
            writer.WriteLine(fourthLine);
            writer.WriteLine(fifthLine);
            writer.WriteLine(sixthLine);
            writer.WriteLine(seventhLine);
            writer.Flush();

            return memoryStream.ToArray();
        }

        [TestMethod]
        public void GameBuilderShowResult()
        {
            var fileName = "gameSettings.txt";
            var output = new StringWriter();
            Console.SetOut(output);

            var memoryStream = new MemoryStream(CreateStreamBytes());
            var gameBuilder =  new GameBuilder(memoryStream, fileName);

            gameBuilder.LoadData();

            StringAssert.Contains(output.ToString(), "Mine Hit");
            StringAssert.Contains(output.ToString(), "Still in Danger");
            StringAssert.Contains(output.ToString(), "Success");
        }
    }
}
