using Microsoft.IdentityModel.Tokens;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SRS.Faculdade.APP.View
{
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TxtError.Text.IsNullOrEmpty())
            {
                TxtError.Text = "Email e/ou senha invalido";
            }

            else if (VerificadorUsuario(email.Text)[1] == "Aluno.edu")
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

        private string[] VerificadorUsuario(string email)
        {
            return email.Split("@");
        }
    }
}
