using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SRS.Faculdade.APP.Model.Pessoa;
using SRS.Faculdade.APP.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SRS.Faculdade.APP.View
{
    public partial class Login : Page
    {
        IUsuarioService _Service = new UsuarioService();
        IList<Usuario> usuarios;

        public Login()
        {
            InitializeComponent();
            usuarios = _Service.ObterTodos();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!email.Text.IsNullOrEmpty() && UsuarioExiste(email.Text, senha.Text))
            {
                if (VerificadorUsuario(email.Text)[1] == "Aluno.edu")
                {
                    var loginPage = AppHost.ServiceProvider.GetRequiredService<Estudante>();
                    ((MainWindow)Application.Current.MainWindow).FramePrincipal.Navigate(loginPage);
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
            Usuario usuario = usuarios.FirstOrDefault(u => u.Email == email);

            if (usuario == null || usuario.Senha != senha)
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
