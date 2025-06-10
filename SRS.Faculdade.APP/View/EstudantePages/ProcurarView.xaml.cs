using SRS.Faculdade.APP.Model.Academico;
using SRS.Faculdade.APP.Model.Entities;
using SRS.Faculdade.APP.Services;
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

namespace SRS.Faculdade.APP.View.EstudantePages
{
    /// <summary>
    /// Interação lógica para ProcurarView.xam
    /// </summary>
    public partial class ProcurarView : Page, INotifyPropertyChanged
    {
        public Estudante Estudante;

        private string _textoBusca;

        private AcademicoService _service;

        private IList<Turma> turmas;

        public string TextoBusca
        {
            get => _textoBusca;
            set
            {
                _textoBusca = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Turma> _resultados;
        public ObservableCollection<Turma> Resultados
        {
            get => _resultados;
            set
            {
                _resultados = value;
                OnPropertyChanged();
            }
        }
        public ProcurarView()
        {
            InitializeComponent();
            DataContext = this;
            var estudante = AppState.EstudanteLogado;
            Estudante = estudante;
            _service = AppState.AcademicoService;
            turmas = _service.ObterTodasTurmas().Where(t => !Estudante.TurmasMatriculadas.Any(m => m.Numero == t.Numero)).ToList();
            Resultados = new ObservableCollection<Turma>(turmas);
        }

        private void Inicio_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).FramePrincipal.Navigate(new EstudanteView());
        }

        private void MenuTurma_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).FramePrincipal.Navigate(new TurmaView());
        }

        private void ProcurarTurma_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).FramePrincipal.Navigate(this);
        }
        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            //// Simulação de busca (substitua com sua lógica real)
            //Resultados.Clear();
            //if (!string.IsNullOrWhiteSpace(TextoBusca))
            //{
            //    Resultados.Add($"Resultado para '{TextoBusca}' - Item 1");
            //    Resultados.Add($"Resultado para '{TextoBusca}' - Item 2");
            //    Resultados.Add($"Resultado para '{TextoBusca}' - Item 3");
            //}
        }


        private void Inscrever_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button botao && botao.Tag is Turma turma)
                {
                    bool jaMatriculado = Estudante.TurmasMatriculadas.Any(t => t.Numero == turma.Numero);
                    if (!jaMatriculado)
                    {
                        Estudante.AdcionarTurma(turma);
                        MessageBox.Show($"Inscrito na turma de {turma.DisciplinaDoCurso.NomeDisciplina}!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        throw new InvalidOperationException("Ja inscrito na turma");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao inscrever na turma: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
