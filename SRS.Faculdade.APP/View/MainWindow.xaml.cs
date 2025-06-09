using SRS.Faculdade.APP.Model.Entities;
using SRS.Faculdade.APP.Services;
using SRS.Faculdade.APP.View;
using SRS.Faculdade.APP.View.EstudantePages;
using System.Windows;

namespace SRS.Faculdade.APP
{
    public partial class MainWindow : Window
    {
        private UsuarioService Service;

        public MainWindow()
        {
            InitializeComponent();

            Service = new UsuarioService();
            Pessoa usuario = Service.ObterPessoaPorId();
            FramePrincipal.Navigate(new EstudanteView((Estudante)usuario));
        }
    }
}