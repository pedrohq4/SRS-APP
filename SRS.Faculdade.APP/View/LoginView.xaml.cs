using Microsoft.IdentityModel.Tokens;
using SRS.Faculdade.APP.Model.Entities;
using SRS.Faculdade.APP.Services;
using SRS.Faculdade.APP.View.AdminPages;
using SRS.Faculdade.APP.View.EstudantePages;
using SRS.Faculdade.APP.View.ProfessorPages;
using System.Windows;
using System.Windows.Controls;
using static SRS.Faculdade.APP.MainWindow;

namespace SRS.Faculdade.APP.View
{
    public partial class LoginView : Page
    {
        UsuarioService _usuarioService;

        public LoginView()
        {
            InitializeComponent();
            _usuarioService = AppState.UsuarioService;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!email.Text.IsNullOrEmpty() && UsuarioExiste(email.Text, senha.Password))
            {
                if (VerificadorUsuario(email.Text)[1] == "Aluno.edu")
                {

                    AppState.EstudanteLogado = (Estudante)_usuarioService.ObterPessoaPorEmail(email.Text);
                    ((MainWindow)Application.Current.MainWindow).FramePrincipal.Navigate(new EstudanteView());
                    Application.Current.MainWindow.WindowState = WindowState.Maximized;
                }

                else if (VerificadorUsuario(email.Text)[1] == "Professor.edu")
                {
                    AppState.ProfessorLogado = (Professor)_usuarioService.ObterPessoaPorEmail(email.Text);
                    ((MainWindow)Application.Current.MainWindow).FramePrincipal.Navigate(new ProfessorView());
                    Application.Current.MainWindow.WindowState = WindowState.Maximized;
                }

                else if (VerificadorUsuario(email.Text)[1] == "Admin.edu")
                {
                    AppState.AdminLogado = (Admin)_usuarioService.ObterPessoaPorEmail(email.Text);
                    ((MainWindow)Application.Current.MainWindow).FramePrincipal.Navigate(new AdminView());
                    Application.Current.MainWindow.WindowState = WindowState.Maximized;
                }
            }
            TxtError.Text = "Email e/ou senha invalido";
        }

        private bool UsuarioExiste(string email, string senha)
        {
            Pessoa pessoa = _usuarioService.ObterPessoaPorEmail(email);

            if (pessoa == null || pessoa.Usuario == null)
            {
                return false;
            }

            if (pessoa.Usuario.Email != email || pessoa.Usuario.Senha != senha)
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
