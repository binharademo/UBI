using UXDivers.Grial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ubi.Controls.Voip;
using UbiModelShared.Connection;
using UbiModelShared.Poco.Voip;
using Xamarin.Forms;
using UbiModelShared.Poco.Chat;

namespace Ubi
{
    public class ChatMessageData
    {
        public ChatUserData From { get; set; }
        public string Body { get; set; }
        public string When { get; set; }
        private int _step = 0;
        public int Step {
            get {
                return _step;
            }
            set {
                _step = value;
                switch (Step) {
                    case 2:
                        ExtraInfo = "Perceba que na BIN nem chamamos de aluguel e sim combo de beneficios. Porque neste combo voce tera 500 bobinas pro mes gratis, sua maquina ira habilitada para recarga de celular e voce ira ganhar 3% de todos os valores. Tem suporte 24 horas e no horario comercial pelo whats. Tem nova maquina em 48 horas se a sua apresentar algum defeito e o mais importante, todas as novas tecnologias como aproximação de cartao, pulseira e celular ja estao na maquina, coisa que nas maquinas compradas sera um problema, pois elas nao serao atualizadas, em nenhum concorrente. E, no final disso tudo ainda e possivel renegociar esse valor na medida que você for melhorando o faturamento na maquina.";
                        //_chatMessages[i].AditionalInfo = "Perceba que na BIN nem chamamos de aluguel e sim combo de benefícios. Porque neste combo você terá 500 bobinas pro mês grátis, sua máquina irá habilitada para recarga de celular e você irá ganhar 3% de todos os valores . Tem suporte 24 horas e no horário comercial pelo whats . Tem nova máquina em 48 horas se a sua apresentar algum defeito e o mais importante , todas as novas tecnologias como aproximação de cartão , pulseira e celular já estão na máquina , coisa que nas máquinas compradas será um problema, pois elas não serão atualizadas, em nenhum concorrente . E, no final disso tudo ainda é possível renegociar esse valor na medida que você for melhorando o faturamento na máquina.".Replace("[NOME DO CLIENTE]", userName);;
                        break;
                    case 3:
                        ExtraInfo = userName+", voce prefere uma ou mais maquinas? Lembrando que alem das taxas voce tera muitos beneficios no nosso COMBO DE VANTAGENS como: 500 bobinas grátis por mes se precisar, basta solicitar via whats. Sua maquina ira habilitada para venda de credito para celular onde seu retorno e de 3% de todos os creditos vendidos. Suporte horario comercial via whats e 24 horas via 0800. Troca da maquininha com defeito em 48 horas. Chip da sua maquina da operadora com melhor sinal. Nao paga transferencia bancaria.";
                        //_chatMessages[i].AditionalInfo = "[NOME DO CLIENTE], você prefere uma ou mais máquinas? Lembrando que além das taxas você terá muitos benefícios no nosso COMBO DE VANTAGENS como: 500 bobinas grátis por mês se precisar, basta solicitar via whats. Sua máquina irá habilitada para venda de crédito para celular onde seu retorno é de 3% de todos os créditos vendidos. Suporte horário comercial via whats e 24 horas via 0800.Troca da maquininha com defeito em 48 horas. Chip da sua máquina da operadora com melhor sinal . Não paga transferência bancária.".Replace("[NOME DO CLIENTE]", userName);
                        break;
                    case 6:
                        ExtraInfo = userName+", voce prefere uma ou mais maquinas? Lembrando que alem das taxas voce tera muitos beneficios no nosso COMBO DE VANTAGENS como: 500 bobinas grátis por mes se precisar, basta solicitar via whats. Sua maquina ira habilitada para venda de credito para celular onde seu retorno e de 3% de todos os creditos vendidos. Suporte horario comercial via whats e 24 horas via 0800. Troca da maquininha com defeito em 48 horas. Chip da sua maquina da operadora com melhor sinal. Nao paga transferencia bancaria.";
                        //_chatMessages[i].AditionalInfo = "[NOME DO CLIENTE], você prefere uma ou mais máquinas? Lembrando que além das taxas você terá muitos benefícios no nosso COMBO DE VANTAGENS como: 500 bobinas grátis por mês se precisar, basta solicitar via whats. Sua máquina irá habilitada para venda de crédito para celular onde seu retorno é de 3% de todos os créditos vendidos. Suporte horário comercial via whats e 24 horas via 0800.Troca da maquininha com defeito em 48 horas. Chip da sua máquina da operadora com melhor sinal . Não paga transferência bancária.".Replace("[NOME DO CLIENTE]", userName);
                        break;
                    case 7:
                        ExtraInfo = userName+", essa proposta de taxa de debito de XX % e crédito YY % Essas taxas fazem sentido pra voce? Lhe interessa?";
                        //_chatMessages[i].AditionalInfo = "[NOME DO CLIENTE], essa proposta de taxa de débito de XX % e crédito YY % Essas taxas fazem sentido pra você? Lhe interessa?".Replace("[NOME DO CLIENTE]", userName);
                        break;
                    default:
                        ExtraInfo = "";
                        break;
                }
            }
        }
        public string userName { get; set; }
        public bool IsRead { get; set; }
        public bool IsInbound { get; set; }
        public bool HasExtraInfo {
            get {
                if (ExtraInfo != null) {
                    return ExtraInfo.Count() > 0;
                } else {
                    return false;
                }

            }
    }
        public bool RequiresUserInfo {
            get {
                switch (Step) {
                    case 4:
                    case 7:
                    case 10:
                    case 21:
                    case 22:
                        return true;
                }
                return false;
            }
        }

        public string ExtraInfo { get; set; }


    }

    public class ChatUserData
    {
        public string Name { get; set; }
        public string Avatar { get; set; }
    }

    public class MessageData
    {
        public ChatUserData From { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool HasAttachment { get; set; }
        public int ThreadCount { get; set; }
        public string When { get; set; }
        public bool IsStared { get; set; }
        public bool IsRead { get; set; }
    }

    public class NotificationData
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public NotificationType Type { get; set; }
    }
}
