using Ubi;
using UbiModelShared.Poco.Chat;
using Xunit;

namespace XUnitTestModel.TestPoco.Chat
{
    public class ChatBotTest
    {

        [Fact]
        public void GetQuestion()
        {
            ChatBot bot = new ChatBot();
            string mess = bot.GetQuestion();
            string result = "Bom dia meu nome é Roberto da BIN maquininha de cartões tudo bem?";
            Assert.Equal(result, mess);
        }

        [Fact]
        public void BotHello()
        {
            ChatBot bot = new ChatBot();
            string mess = bot.GetBotResponse(UserResponseEnum.Yes);
            string result = "estamos com uma excelente campanha de taxa de débito de XX % e crédito YY % Essas taxas fazem sentido pra você?";
            Assert.Equal(result,mess);

        }
        [Fact]
        public void BotPasso1Nao()
        {
            ChatBot bot = new ChatBot();
            string mess = bot.GetBotResponse(UserResponseEnum.No);
            string result = "Bom dia meu nome é [Seu Nome] da BIN maquininha de cartões tudo bem?";
            Assert.Equal(result, mess);
        }

        [Fact]
        public void BotDandoDoisPassos()
        {
            ChatBot bot = new ChatBot();
            string mess1 = bot.GetBotResponse(UserResponseEnum.Yes);
            string mess2 = bot.GetBotResponse(UserResponseEnum.Yes);
            string result = "FULANOnvbvvhghghhfhf, estamos com uma excelente campanha de taxa de d�bito de XX % e cr�dito YY % Essas taxas fazem sentido pra voc�?";
            Assert.Equal(result, mess2);
        }

        [Fact]
        public void BotDandoDoisPassosNao()
        {
            ChatBot bot = new ChatBot();
            string mess1 = bot.GetBotResponse(UserResponseEnum.No);
            string mess2 = bot.GetBotResponse(UserResponseEnum.No);
            string result = "Pergunte quem est� falando e questione se ele conhece o [NOME DO CLIENTE].";
            Assert.Equal(result, mess2);
        }
    }
}
