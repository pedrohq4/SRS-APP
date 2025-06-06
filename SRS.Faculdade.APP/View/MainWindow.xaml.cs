using Microsoft.Extensions.DependencyInjection;
using SRS.Faculdade.APP.View;
using System.Windows;

namespace SRS.Faculdade.APP
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var loginPage = AppHost.ServiceProvider.GetRequiredService<Login>();
            FramePrincipal.Navigate(loginPage);
        }
    }
}