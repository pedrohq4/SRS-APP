using LiveCharts;
using LiveCharts.Wpf;
using SRS.Faculdade.APP.Model.Academico;
using SRS.Faculdade.APP.Model.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;
using static SRS.Faculdade.APP.MainWindow;

namespace SRS.Faculdade.APP.View.ProfessorPages
{
    public partial class ProfessorView : Page, INotifyPropertyChanged
    {
        private ObservableCollection<Turma> _turmas;
        private SeriesCollection _seriesGeral;
        private string[] _labelsTurmas;
        private Professor _professorLogado;
        private SeriesCollection _seriesBarras = new SeriesCollection();
        private SeriesCollection _seriesPizzas = new SeriesCollection();
        public SeriesCollection SeriesBarras
        {
            get => _seriesBarras;
            set
            {
                _seriesBarras = value;
                OnPropertyChanged(nameof(SeriesBarras));
            }
        }

        public SeriesCollection SeriesPizzas
        {
            get => _seriesPizzas;

            set
            {
                _seriesPizzas = value;
                OnPropertyChanged(nameof(SeriesPizzas));
            }
        }

        public ProfessorView()
        {
            InitializeComponent();
            DataContext = this;

            FormatadorEixoY = value => value.ToString("N0");

            _professorLogado = AppState.ProfessorLogado;
            InicializarDados();
            CarregarGraficoGeral();
            CarregarGraficoPizza();
            CarregarGraficoBarras();
        }

        public ObservableCollection<Turma> Turmas
        {
            get => _turmas;
            set
            {
                _turmas = value;
                OnPropertyChanged(nameof(Turmas));
            }
        }

        public SeriesCollection SeriesGeral
        {
            get => _seriesGeral;
            set
            {
                _seriesGeral = value;
                OnPropertyChanged(nameof(SeriesGeral));
            }
        }

        public string[] LabelsTurmas
        {
            get => _labelsTurmas;
            set
            {
                _labelsTurmas = value;
                OnPropertyChanged(nameof(LabelsTurmas));
            }
        }

        public Func<double, string> FormatadorEixoY { get; set; }

        private void InicializarDados()
        {
            if (_professorLogado?.TurmasLecionadas == null) return;

            Turmas = new ObservableCollection<Turma>(_professorLogado.TurmasLecionadas.Values.ToList());

            LabelsTurmas = Turmas.Select(t => t.Nome).ToArray();
        }

        private void CarregarGraficoPizza()
        {
            if (Turmas == null || Turmas.Count == 0) return;

            int aprovados = 0;
            int reprovados = 0;

            foreach (var turma in Turmas)
            {
                foreach (var estudante in turma.EstudantesInscritos.Values)
                {
                    var historicoTurma = estudante.Historico?.Turmas.FirstOrDefault(t => t.Turma == turma);
                    if (historicoTurma != null)
                    {
                        if (historicoTurma.EstaAprovado())
                            aprovados++;
                        else
                            reprovados++;
                    }
                }
            }

            SeriesPizzas = new SeriesCollection
            {
               new PieSeries
               {
                   Title = "Aprovados",
                   Values = new ChartValues<double> { aprovados },
                   DataLabels = true,
                   Fill = Brushes.Green,
                   LabelPoint = point => $"{point.Y} ({point.Participation:P0})"
               },
               new PieSeries
               {
                   Title = "Reprovados",
                   Values = new ChartValues<double> { reprovados },
                   DataLabels = true,
                   Fill = Brushes.Red,
                   LabelPoint = point => $"{point.Y} ({point.Participation:P0})"
               }
            };
        }

        private void CarregarGraficoGeral()
        {
            if (Turmas == null || Turmas.Count == 0) return;

            var mediasNotasTurmas = CalcularMediasNotasPorTurma();

            SeriesGeral = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Média de Notas por Turma",
                    Values = new ChartValues<double>(mediasNotasTurmas),
                    Fill = Brushes.LightBlue,
                    Stroke = Brushes.Blue,
                    StrokeThickness = 1
                }
            };
        }

        private void CarregarGraficoBarras()
        {
            if (Turmas == null || Turmas.Count == 0) return;

            var tempSeries = new SeriesCollection();
            var categorias = new[] { "ND", "D", "DM", "DML" };

            foreach (var categoria in categorias)
            {
                var valores = new ChartValues<int>();

                foreach (var turma in Turmas)
                {
                    valores.Add(ContarConceitosPorTurma(turma, categoria));
                }

                tempSeries.Add(new ColumnSeries
                {
                    Title = categoria,
                    Values = valores,
                    DataLabels = true
                });
            }

            SeriesBarras = tempSeries;
        }

        private int ContarConceitosPorTurma(Turma turma, string conceitoDesejado)
        {
            int count = 0;

            foreach (var estudante in turma.EstudantesInscritos.Values)
            {
                var conceitos = ObterConceitosEstudanteNaTurma(estudante, turma);
                count += conceitos.Count(c => c.Equals(conceitoDesejado, StringComparison.OrdinalIgnoreCase));
            }

            return count;
        }

        private List<double> CalcularMediasNotasPorTurma()
        {
            var medias = new List<double>();

            foreach (var turma in Turmas)
            {
                var mediaNotasTurma = CalcularMediaNotasTurma(turma);
                medias.Add(mediaNotasTurma);
            }

            return medias;
        }

        private double CalcularMediaNotasTurma(Turma turma)
        {
            if (turma?.EstudantesInscritos == null || turma.EstudantesInscritos.Count == 0)
                return 0;

            double somaNotas = 0;
            int totalNotas = 0;

            foreach (var estudante in turma.EstudantesInscritos.Values)
            {
                var conceitos = ObterConceitosEstudanteNaTurma(estudante, turma);

                foreach (var conceito in conceitos)
                {
                    // Usando o método público para obter o valor do conceito
                    int valorNota = HistoricoTurma.ObterValorDoConceito(conceito);
                    somaNotas += valorNota;
                    totalNotas++;
                }
            }

            return totalNotas > 0 ? somaNotas / totalNotas : 0;
        }

        private List<string> ObterConceitosEstudanteNaTurma(Estudante estudante, Turma turma)
        {
            var conceitos = new List<string>();

            if (estudante == null || !estudante.EstaMatriculadoEm(turma) || estudante.Historico == null)
            {
                return conceitos;
            }

            foreach (var historicoTurma in estudante.Historico.Turmas)
            {
                if (historicoTurma.Turma == turma)
                {
                    // Verifica e adiciona apenas notas válidas
                    AdicionarSeNotaValida(historicoTurma.Nota1, conceitos);
                    AdicionarSeNotaValida(historicoTurma.Nota2, conceitos);
                    AdicionarSeNotaValida(historicoTurma.Nota3, conceitos);
                    AdicionarSeNotaValida(historicoTurma.Nota4, conceitos);
                }
            }

            return conceitos;
        }
        private void AdicionarSeNotaValida(string nota, List<string> conceitos)
        {
            if (!string.IsNullOrEmpty(nota) && HistoricoTurma.EhNotaValida(nota))
            {
                conceitos.Add(nota.ToUpper());
            }
        }
        public void RecarregarDados()
        {
            InicializarDados();
            CarregarGraficoGeral();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}