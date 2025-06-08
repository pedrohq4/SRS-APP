using Microsoft.IdentityModel.Tokens;
using SRS.Faculdade.APP.Model.Entities;
using SRS.Faculdade.APP.Services;
using SRS.Faculdade.APP.View.EstudantePages;
using System.Windows;
using System.Windows.Controls;

namespace SRS.Faculdade.APP.View
{
    public partial class LoginView : Page
    {
        UsuarioService _Service = new UsuarioService();
        Pessoa Pessoa;
        private Estudante estudante;
        private Professor professor;

        public LoginView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!email.Text.IsNullOrEmpty() && UsuarioExiste(email.Text, senha.Password))
            {
                if (VerificadorUsuario(email.Text)[1] == "Aluno.edu")
                {
                    ((MainWindow)Application.Current.MainWindow).FramePrincipal.Navigate(new EstudanteView((Estudante)Pessoa));
                    Application.Current.MainWindow.WindowState = WindowState.Maximized;
                }

                else if (VerificadorUsuario(email.Text)[1] == "Professor.edu")
                {

                }

                else if (VerificadorUsuario(email.Text)[1] == "Adm.edu")
                {

                }
            }
            TxtError.Text = "Email e/ou senha invalido";
        }

        private bool UsuarioExiste(string email, string senha)
        {
            Pessoa = _Service.ObterPessoaPorEmail(email);

            if (Pessoa == null || Pessoa.Usuario == null)
            {
                return false;
            }

            if (Pessoa.Usuario.Email != email || Pessoa.Usuario.Senha != senha)
            {
                return false;
            }

            return true;
        }

        private string[] VerificadorUsuario(string email)
        {
            return email.Split("@");
        }
    }
}
