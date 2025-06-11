using SRS.Faculdade.APP.Model.Academico;
using SRS.Faculdade.APP.Model.Entities;
using SRS.Faculdade.APP.View.EstudantePages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static SRS.Faculdade.APP.MainWindow;

namespace SRS.Faculdade.APP.View.ProfessorPages
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

        public Professor Professor { get; set; }

        public TurmaView()
        {
            DataContext = this;
            Professor = AppState.ProfessorLogado;
            //TurmasVisiveis = new ObservableCollection<Turma>(Professor.TurmasLecionadas.Values);
            TurmasVisiveis = new ObservableCollection<Turma>()
            {
                new Turma(201, DayOfWeek.Thursday,"302",32, 20,new Curso("MT", "Matematica", 30), new Professor(101, "João", "Cuz", "23", "Baicharel", "Humanas")),
                new Turma(202, DayOfWeek.Monday, "205", 25, 20, new Curso("FS", "Física", 25), new Professor(101, "João", "Cuz", "23", "Baicharel", "Humanas")),
                new Turma(203, DayOfWeek.Tuesday, "110", 30, 20, new Curso("HT", "História", 30), new Professor(101, "João", "Cuz", "23", "Baicharel", "Humanas")),
                new Turma(204, DayOfWeek.Wednesday, "Lab3", 20, 20, new Curso("QM", "Química", 20), new Professor(101, "João", "Cuz", "23", "Baicharel", "Humanas")),
            };
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void Inicio_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).FramePrincipal.Navigate(new ProfessorView());
        }

        private void Turmas_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).FramePrincipal.Navigate(this);
        }

        private void Detalhes_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            var turmaSelecionada = button?.CommandParameter as Turma;

            if (turmaSelecionada != null)
            {
                ((MainWindow)Application.Current.MainWindow).FramePrincipal.Navigate(new DetalheView(turmaSelecionada));
            }
        }
    }
}
