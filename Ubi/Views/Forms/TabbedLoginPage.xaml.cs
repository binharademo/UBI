using System;
using Xamarin.Forms;
using UXDivers.Grial;
using UbiModelShared.Poco;
using UbiModelShared.Connection;
using UbiModelShared.Validation;
using UbiModelShared.Poco.Voip;
using Realms.Sync;
using Ubi.Controls.Voip;

namespace Ubi
{
    public partial class TabbedLoginPage : ContentPage
    {
        public TabbedLoginPage()
        {
            InitializeComponent();

            // Login
            tfEmailLogin.ReturnCommand = new Command(() => tfPasswordLogin.Focus()); // faz quando clicar em NEXT ir para a entry da Password

            // SignUp
            tfNameSignup.ReturnCommand = new Command(() => tfEmailSignup.Focus());    // faz quando clicar em NEXT ir para a entry do Email
            tfEmailSignup.ReturnCommand = new Command(() => tfCpfSignup.Focus());     // faz quando clicar em NEXT ir para a entry da Cpf
            tfCpfSignup.ReturnCommand = new Command(() => tfPasswordSignup.Focus());  // faz quando clicar em NEXT ir para a entry da Password
            tfPasswordSignup.ReturnCommand = new Command(() => tfCodigoPaisSignup.Focus());  // faz quando clicar em NEXT ir para a entry do DDD
            tfCodigoPaisSignup.ReturnCommand = new Command(() => tfDDDSignup.Focus());  // faz quando clicar em NEXT ir para a entry do DDD
            tfDDDSignup.ReturnCommand = new Command(() => tfNumberSignup.Focus());    // faz quando clicar em NEXT ir para a entry do Numero do celular

        }


        // ====== Checando informações para fazer login ====== //

        private async void Check_Info_Login(object sender, EventArgs e)
        {
            if (new IsNotNullOrEmptyRule<string>().Check(tfEmailLogin.Text) &&
                new IsNotNullOrEmptyRule<string>().Check(tfPasswordLogin.Text))
                try
                {
                    UserVoip u = ConectionRealm.getInstance().getFazLogin(tfEmailLogin.Text, tfPasswordLogin.Text);
                    VoipControler.getInstance(null).setLogin(u);
                    if (u != null){
                        App.Current.MainPage = new RootMasterDetailPage();
                    }
                    else{
                        await DisplayAlert("Ops!", "Login ou senha incorretos, tente novamente.", "Ok");
                    }

                }
                catch (Exception ex)
                {
                    await DisplayAlert("Ops!", ex.Message, "Ok");
                }
                else
                Incomplete_Info();
        }

        // ====== Checando informações para fazer cadastro ====== //

        private async void Check_Info_SignUp(object sender, EventArgs e)
        {

            string numberUser = tfCodigoPaisSignup.Text + tfDDDSignup.Text + tfNumberSignup.Text;

            string firstNameToLower;
            string NameSignupToLower;

            char[] strSep = { ' ' };
            string[] strArray = tfNameSignup.Text.Split(strSep);
            string firstNameUser = strArray[0];
            string fullNameUser = tfNameSignup.Text;


            System.Globalization.CultureInfo cultureinfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            firstNameToLower = firstNameUser;
            firstNameUser = firstNameToLower.ToLower();
            firstNameUser = cultureinfo.TextInfo.ToTitleCase(firstNameUser);


            NameSignupToLower = tfNameSignup.Text;
            tfNameSignup.Text = NameSignupToLower.ToLower();
            tfNameSignup.Text = cultureinfo.TextInfo.ToTitleCase(tfNameSignup.Text);


            if (new IsNotNullOrEmptyRule<string>().Check(tfNameSignup.Text) &&
                new IsNotNullOrEmptyRule<string>().Check(tfEmailSignup.Text) &&
                new IsNotNullOrEmptyRule<string>().Check(tfCpfSignup.Text) &&
                new IsNotNullOrEmptyRule<string>().Check(tfPasswordSignup.Text) &&
                new IsNotNullOrEmptyRule<string>().Check(tfCodigoPaisSignup.Text) &&
                new IsNotNullOrEmptyRule<string>().Check(tfDDDSignup.Text) &&
                new IsNotNullOrEmptyRule<string>().Check(tfNumberSignup.Text) &&
                new NameValidator<string>().Check(tfNameSignup.Text) &&
                new EmailValidator<string>().Check(tfEmailSignup.Text) &&
                new CpfValidator<string>().Check(tfCpfSignup.Text) &&
                //new UserPassValidator<string>().Check(tfPasswordSignup.Text) &&
                new CellphoneValidator<string>().Check(tfNumberSignup.Text))
                try
                {

                    ConectionRealm con = ConectionRealm.getInstance();
                    UserVoip u = new UserVoip();
                    u.Name = firstNameUser + "";
                    u.FullName = tfNameSignup.Text;
                    u.Email = tfEmailSignup.Text;
                    u.Cpf = tfCpfSignup.Text;
                    u.PassWord = tfPasswordSignup.Text;
                    u.Cellphone = numberUser;
                    u.Id = Guid.NewGuid().ToString();
                    u.ServerVoip = "170.245.200.107";
                    u.OnlineTime = 0;
                    u.AnsweredCalls = 0;
                    u.ClosedSales = 0;
                    u.ConversionRate = 0;
                    int n = con.getUserVoipCount();
                    u.BranchLineNumber = (2005 + n%4)+"";

                    con.AddEntry(u);


                    await DisplayAlert("Sucesso!", "Conta cadastrada.", "Ok");

                    VoipControler.getInstance(null).setLogin(u);
                    App.Current.MainPage = new RootMasterDetailPage();
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Ops!", ex.Message, "Ok");
                }
            else if (new IsNullOrEmptyRule<string>().Check(tfNameSignup.Text)      ||
                     new IsNullOrEmptyRule<string>().Check(tfEmailSignup.Text)     ||
                     new IsNullOrEmptyRule<string>().Check(tfCpfSignup.Text)       ||
                     new IsNullOrEmptyRule<string>().Check(tfPasswordSignup.Text)  ||
                     new IsNullOrEmptyRule<string>().Check(tfCodigoPaisSignup.Text)||
                     new IsNullOrEmptyRule<string>().Check(tfDDDSignup.Text)       ||
                     new IsNullOrEmptyRule<string>().Check(tfNumberSignup.Text))
            {
                Incomplete_Info();
            }
            else if (new NameValidator<string>().Check(tfNameSignup.Text) == false)
            {
                Invalid_Name();
            }
            else if (new EmailValidator<string>().Check(tfEmailSignup.Text) == false)
            {
                Invalid_Email();
            }
            else if (new CpfValidator<string>().Check(tfCpfSignup.Text) == false)
            {
                Invalid_CPF();
            }
            //else if (new UserPassValidator<string>().Check(tfPasswordSignup.Text) == false) ====== //Não está com validação
            //{
            //    Invalid_Password();
            //}
            else if (new CellphoneValidator<string>().Check(tfNumberSignup.Text) == false)
            {
                Invalid_Cellphone();
            }

        }

        private void Invalid_Name()
        {
            DisplayAlert("Ops!", "Não utilize caracteres especiais no nome.", "Ok");
        }

        private void Invalid_Email()
        {
            DisplayAlert("Ops!", "Digite um e-mail válido.", "Ok");
        }

        private void Invalid_CPF()
        {
            DisplayAlert("Ops!", "Digite um CPF válido.", "Ok");
        }

        private void Invalid_Password()
        {
            DisplayAlert("Ops!", "Digite um senha válida válido.", "Ok");
        }

        private void Invalid_Cellphone()
        {
            DisplayAlert("Ops!", "Insira um número de telefone válido.", "Ok");
        }

        private void Incomplete_Info()
        {
            DisplayAlert("Ops!", "Preencha todos os campos antes de continuar.", "Ok");
        }

        private void Entrar_Facebook_Button_Clicked(object sender, EventArgs e) {
            //TODO
        }

        private void Cadastrar_Facebook_Button_Clicked(object sender, EventArgs e) {
            //TODO
        }
    }
}
