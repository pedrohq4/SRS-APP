using SRS.Faculdade.APP.Model.Academico;
using SRS.Faculdade.APP.Model.Entities;
using SRS.Faculdade.APP.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using static SRS.Faculdade.APP.MainWindow;

namespace SRS.Faculdade.APP.View.AdminPages
{
    public partial class ProcurarView : Page, INotifyPropertyChanged
    {
        public Admin Admin;

        private string _textoBusca;

        private AcademicoService _AcademicoService;

        private UsuarioService _UsuarioService;

        private IList<Turma> Turmas;
        private IList<Pessoa> Pessoas;
        
        public string TextoBusca
        {
            get => _textoBusca;
            set
            {
                _textoBusca = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Turma> _resultadosTurma;
        public ObservableCollection<Turma> ResultadosTurma
        {
            get => _resultadosTurma;
            set
            {
                _resultadosTurma = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Pessoa> _resultadosPessoas;
        public ObservableCollection<Pessoa> ResultadosPessoas
        {
            get => _resultadosPessoas;

            set
            {
                _resultadosPessoas = value;
                OnPropertyChanged();
            }
        }
        public ProcurarView()
        {
            _UsuarioService = AppState.UsuarioService;
            _AcademicoService = AppState.AcademicoService;
            DataContext = this;
            Admin = AppState.AdminLogado;
            Turmas = _AcademicoService.ObterTodasTurmas();
            if (Turmas != null)
            {
                ResultadosTurma = new ObservableCollection<Turma>(Turmas);
            }
            InitializeComponent();
        }

        private void Inicio_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).FramePrincipal.Navigate(new AdminView());
        }

        private void ProcurarTurma_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).FramePrincipal.Navigate(this);
        }

        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            if (rbTurmas.IsChecked == true)
            {
                ResultadosTurma.Clear();

                if (!string.IsNullOrWhiteSpace(TextoBusca))
                {
                    var resultadosFiltrados = Turmas
                        .Where(t => t.DisciplinaDoCurso != null &&
                                    t.DisciplinaDoCurso.NomeDisciplina != null &&
                                    t.DisciplinaDoCurso.NomeDisciplina
                                        .ToLower()
                                        .Contains(TextoBusca.ToLower()))
                        .ToList();

                    foreach (var turma in resultadosFiltrados)
                    {
                        ResultadosTurma.Add(turma);
                    }
                }
                else
                {
                    foreach (var turma in Turmas)
                    {
                        ResultadosTurma.Add(turma);
                    }
                }
            }

            else if (rbTurmas.IsChecked == true) 
            {
                ResultadosPessoas.Clear();

                if (!string.IsNullOrWhiteSpace(TextoBusca))
                {
                    var resultadosFiltrados = Pessoas
                        .Where(p => (!string.IsNullOrWhiteSpace(p.Nome) && p.Nome.ToLower().Contains(TextoBusca.ToLower())) ||
                                    (!string.IsNullOrWhiteSpace(p.SobreNome) && p.SobreNome.ToLower().Contains(TextoBusca.ToLower())))
                        .ToList();

                    foreach (var pessoa in resultadosFiltrados)
                    {
                        ResultadosPessoas.Add(pessoa);
                    }
                }
                else
                {
                    foreach (var pessoa in Pessoas)
                    {
                        ResultadosPessoas.Add(pessoa);
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (rbTurmas.IsChecked == true)
            {
                var turmasList = _AcademicoService.ObterTodasTurmas();

                ResultadosTurma = turmasList != null
                    ? new ObservableCollection<Turma>(turmasList)
                    : new ObservableCollection<Turma>();
            }
            else if (rbPessoas.IsChecked == true)
            {
                var pessoasList = _UsuarioService.ObterTodos();

                ResultadosPessoas = pessoasList != null
                    ? new ObservableCollection<Pessoa>(pessoasList)
                    : new ObservableCollection<Pessoa>();
            }
        }
    }
}
