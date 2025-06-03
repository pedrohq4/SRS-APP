using SRS.Faculdade.APP.View;
using System.Windows;

namespace SRS.Faculdade.APP
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FramePrincipal.Navigate(new Login());
        }
    }
}