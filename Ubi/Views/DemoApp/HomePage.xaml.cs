using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ubi.Controls.Voip;
using UbiModelShared.Beans;
using UbiModelShared.Connection;
using UbiModelShared.Poco.Voip;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ubi.Views.DemoApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public void awake()
        {

        }

        VoipControler vc;
        public HomePage()
        {
            InitializeComponent();

            CriandoLista();

            ValoresBarrasDeProgresso();

            vc = VoipControler.getInstance(this);
            Switch01.BindingContext = this;
            vc.lb_status = lb_status_atendimento;
            if (AtendimentoAtivado)
            {
                vc.lb_status.Text = "Atendimento Ativado";
                vc.Register();
            }
            else {
                vc.lb_status.Text = "Atendimento Desativado";
                vc.lb_status.TextColor = Color.Red;
                vc.CancelRegister();
            }

        }

        ObservableCollection<Product> lista_de_produtos;

        public void CriandoLista() {
            repeater.ItemsSource = lista_de_produtos = ConectionRealm.getInstance().getProducts();
        }

        public double horasOnline;
        public double atendimentosFeitos;
        public double fechamentos;
        public double taxaDeConversao;

        public double progressoHorasOnline = 0;        //barra
        public double progressoAtentimentosFeitos = 0; //barra
        public double progressoFechamentos = 0;        //barra
        public double progressoTaxaDeConversao = 0;    //barra


        public void ValoresBarrasDeProgresso() {

            int metaHora = 10;           //placeholder
            int metaAtendimento = 10;    //placeholder
            int metaFechamento = 10;     //placeholder
            int metaTaxa = 10;           //placeholder


            //Horas Online ==================================================================
            horasOnline = VoipControler.getInstance(null).GetUserVoip().OnlineTime;

            if (horasOnline <= metaHora) {
                progressoHorasOnline = horasOnline / 10;
            } else if (horasOnline > metaHora && horasOnline <= metaHora * 2) {
                metaHora = metaHora * 2;
                progressoHorasOnline = horasOnline / 20;
            } else if (horasOnline > metaHora * 2 && horasOnline <= metaHora * 3) {
                metaHora = metaHora * 3;
                progressoHorasOnline = horasOnline / 30;
            }
            tfHoras.Text = horasOnline + "/" + metaHora + " horas";
            tfBarraProgressoOnline.Progress = progressoHorasOnline;

            //Atendimentos Feitos ==================================================================
            atendimentosFeitos = VoipControler.getInstance(null).GetUserVoip().AnsweredCalls;

            if (atendimentosFeitos <= metaAtendimento) {
                progressoAtentimentosFeitos = atendimentosFeitos / 10;
            } else if (atendimentosFeitos > metaAtendimento && atendimentosFeitos <= metaAtendimento * 2) {
                metaAtendimento = metaAtendimento * 2;
                progressoAtentimentosFeitos = atendimentosFeitos / 20;
            } else if (atendimentosFeitos > metaAtendimento * 2 && atendimentosFeitos <= metaAtendimento * 3) {
                metaAtendimento = metaAtendimento * 3;
                progressoAtentimentosFeitos = atendimentosFeitos / 30;
            }

            tfAtentimentos.Text = atendimentosFeitos + "/" + metaAtendimento + " horas";
            tfBarraProgressoAtendimento.Progress = progressoAtentimentosFeitos;

            //Fechamentos ==================================================================
            fechamentos = VoipControler.getInstance(null).GetUserVoip().ClosedSales;

            if (fechamentos <= metaFechamento) {
                progressoFechamentos = fechamentos / 10;
            } else if (fechamentos > metaFechamento && fechamentos <= metaFechamento * 2) {
                metaFechamento = metaFechamento * 2;
                progressoFechamentos = fechamentos / 20;
            } else if (fechamentos > metaFechamento && fechamentos <= metaFechamento * 3) {
                metaFechamento = metaFechamento * 3;
                progressoFechamentos = fechamentos / 30;
            }

            tfFechamentos.Text = fechamentos + "/" + metaFechamento;
            tfBarraProgressoFechamento.Progress = progressoFechamentos;

            //Taxa de Conversão ==================================================================
            taxaDeConversao = VoipControler.getInstance(null).GetUserVoip().ConversionRate;

            if (taxaDeConversao <= metaTaxa) {
                progressoTaxaDeConversao = taxaDeConversao / 10;
            } else if (taxaDeConversao > metaTaxa && taxaDeConversao <= metaTaxa * 2) {
                metaTaxa = metaTaxa * 2;
                progressoTaxaDeConversao = taxaDeConversao / 20;
            } else if (taxaDeConversao > metaFechamento && taxaDeConversao <= metaTaxa * 3) {
                metaTaxa = metaTaxa * 3;
                progressoTaxaDeConversao = taxaDeConversao / 30;
            }

            tfConversao.Text = taxaDeConversao + "%/" + metaTaxa + "%";
            tfBarraProgressoTaxa.Progress = progressoTaxaDeConversao;
        }


        // testes barras de progresso /////////////


        //public int horasOnlineI;
        //public int atendimentosFeitosI;
        //public int fechamentosI;
        //public int taxaDeConversaoI;

        //public void setar(object sender, EventArgs e) {
        //    horasOnlineI = 3;
        //    atendimentosFeitosI = 12;
        //    fechamentosI = 7;
        //    taxaDeConversaoI = 21;

        //    try {

        //        ConectionRealm cr = ConectionRealm.getInstance();
        //        UserVoip loggeduser = VoipControler.getInstance(null).GetUserVoip();
        //        cr.Setar(loggeduser, horasOnlineI, atendimentosFeitosI, fechamentosI, taxaDeConversaoI);

        //    } catch (Exception es) {
        //        Console.WriteLine("Erro na escrita " + es);
        //        throw;
        //    }

        //}

        public bool AtendimentoAtivado
        {
            get
            {
                return vc.receiving;
            }
            set
            {
                if (value) {
                    vc.lb_status.Text = "Registrando...";
                    vc.lb_status.TextColor = Color.Black;
                    vc.Register();
                } else
                {
                    vc.CancelRegister();
                    vc.lb_status.Text = "Atendimento Desativado";
                }

                vc.receiving = value;
            }
        }

        private void Switch01_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Switch01.OnColor = Color.ForestGreen;
        }

        private void Switch01_PropertyChanged_1(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
        }
    }



    public class CustomProgressBar : ProgressBar
    {
        public CustomProgressBar()
        {

        }
    }
    
}