using SRS.Faculdade.APP.Model.Entities;
using SRS.Faculdade.APP.Services;
using SRS.Faculdade.APP.View;
using SRS.Faculdade.APP.View.EstudantePages;
using System.Windows;

namespace SRS.Faculdade.APP
{
    public partial class MainWindow : Window
    {
        public static class AppState
        {
            public static Estudante EstudanteLogado { get; set; }
            public static AcademicoService AcademicoService { get; set; }
            public static UsuarioService UsuarioService { get; set; }
        }
        public MainWindow()
        {
            InitializeComponent();
            AppState.AcademicoService = new AcademicoService();
            AppState.UsuarioService = new UsuarioService();
            FramePrincipal.Navigate(new LoginView());
        }
    }
}