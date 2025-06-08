using SRS.Faculdade.APP.Model.Pessoa;
using SRS.Faculdade.APP.Services;
using SRS.Faculdade.APP.View;
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
            Usuario Usuario = Service.Obter();
            FramePrincipal.Navigate(new EstudanteView(Usuario));
        }
    }
}