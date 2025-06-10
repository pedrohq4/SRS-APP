using SRS.Faculdade.APP.Model.Academico;
using SRS.Faculdade.APP.Model.Entities;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using static SRS.Faculdade.APP.MainWindow;

namespace SRS.Faculdade.APP.View.EstudantePages
{
    public partial class TurmaView : Page, INotifyPropertyChanged
    {
        private ObservableCollection<Turma> _turmasVisiveis;
        public ObservableCollection<Turma> TurmasVisiveis
        {
            get => _turmasVisiveis;
            set
            {
                _turmasVisiveis = value;
                OnPropertyChanged();
            }
        }

        public Estudante Estudante { get; set; }

        public TurmaView()
        {
            InitializeComponent();
            DataContext = this;
            Estudante = AppState.EstudanteLogado;
            TurmasVisiveis = new ObservableCollection<Turma>(Estudante.TurmasMatriculadas);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void Inicio_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).FramePrincipal.Navigate(new EstudanteView());
        }

        private void MenuTurma_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).FramePrincipal.Navigate(this);
        }

        private void ProcurarTurma_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).FramePrincipal.Navigate(new ProcurarView());
        }
    }
}
