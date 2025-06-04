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
        IPessoaService _Service = new PessoaService();

        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!email.Text.IsNullOrEmpty() && UsuarioExistente(email.Text, senha.Text))
            {
                if (VerificadorUsuario(email.Text)[1] == "Aluno.edu")
                {
                    ((MainWindow)Application.Current.MainWindow).FramePrincipal.Navigate(new Aluno());
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

        private bool UsuarioExistente(string email, string senha)
        {
            var usuarios = _Service.ObterTodos();
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
