using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using SRS.Faculdade.APP.Model.Academico;
using SRS.Faculdade.APP.Model.Entities;

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

        public TurmaView(Estudante estudante)
        {
            InitializeComponent();
            Estudante = estudante;

            if (Estudante.TurmasMatriculadas == null || Estudante.TurmasMatriculadas.Count == 0)
            {
                Estudante.AdcionarTurma(new Turma(
                    201,
                    DayOfWeek.Thursday,
                    "10:30",
                    "302",
                    32,
                    new Curso("DR1", "Matematica", 30),
                    new Professor("João", "Silva", "0", "Professor", "Humanas")
                ));
            }

            TurmasVisiveis = new ObservableCollection<Turma>(Estudante.TurmasMatriculadas);
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
