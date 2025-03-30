using System;
using System.Collections.Generic;
using System.Text;
using UbiModelShared.Poco.Chat;
using Xunit;

namespace XUnitTestModel.TestPoco.Chat
{
    public class ReadJsonDataTest
    {

        [Fact]
        public void LoadJsonFile()
        {
            ReadJsonData rd = new ReadJsonData();
            var result = rd.LoadJsonFileAsync();
            Assert.True(result);

        }

        [Fact]
        public void ParseDataStep()
        {
            ReadJsonData rd = new ReadJsonData();
            var result = rd.ChatMessage(3);
            Assert.Equal(3,result.Step);
        }

        [Fact]
        public void ParseDataQuestion()
        {
            ReadJsonData rd = new ReadJsonData();
            var result = rd.ChatMessage(2);
            Assert.Equal("FULANO, estamos com uma excelente campanha de taxa de d�bito de XX % e cr�dito YY % Essas taxas fazem sentido pra voc�?", result.Question);
        }

        [Fact]
        public void ParseDataStatus()
        {
            ReadJsonData rd = new ReadJsonData();
            var result = rd.ChatMessage(30);

            Assert.Equal("CLOSE", result.Status);
        }


    }
}
