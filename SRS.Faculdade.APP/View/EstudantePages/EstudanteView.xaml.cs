using LiveCharts;
using LiveCharts.Wpf;
using SRS.Faculdade.APP.Model.Entities;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static SRS.Faculdade.APP.MainWindow;

namespace SRS.Faculdade.APP.View.EstudantePages
{
    public partial class EstudanteView : Page, INotifyPropertyChanged
    {
        public Estudante Estudante { get; set; }
        public string Saudacao => $"Bem vindo, {Estudante.Nome}!";

        private SeriesCollection _presencaSeries;
        public SeriesCollection PresencaSeries
        {
            get => _presencaSeries;
            set
            {
                _presencaSeries = value;
                OnPropertyChanged();
            }
        }

        private List<string> _labelsMeses;
        public List<string> LabelsMeses
        {
            get => _labelsMeses;
            set
            {
                _labelsMeses = value;
                OnPropertyChanged();
            }
        }

        private Func<double, string> _formatadorEixoY;
        public Func<double, string> FormatadorEixoY
        {
            get => _formatadorEixoY;
            set
            {
                _formatadorEixoY = value;
                OnPropertyChanged();
            }
        }

        public EstudanteView()
        {
            InitializeComponent();
            DataContext = this;
            var estudante = AppState.EstudanteLogado;
            Estudante = estudante;

            PresencaSeries = new SeriesCollection();
            LabelsMeses = new List<string>();
            FormatadorEixoY = valor => valor + "%";

            CarregarDadosGrafico();
        }

        private void CarregarDadosGrafico()
        {
            //var valores = new ChartValues<double>(Estudante.TurmasMatriculadas.Select(t => t.TotalAulas > 0 ? (double)t.Presenca / t.TotalAulas * 100 : 0));
            var random = new Random();
            var valores = new ChartValues<double>();

            for (int i = 0; i < 6; i++)
            {
                valores.Add(random.Next(70, 100)); // Valores entre 70% e 100%
            }
            var meses = new List<string> { "Jan", "Fev", "Mar", "Abr", "Mai", "Jun" };

            PresencaSeries.Add(new ColumnSeries
            {
                Title = "Presenças",
                Values = valores,
                Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3F51B5")),
                Stroke = new SolidColorBrush(Colors.White),
                StrokeThickness = 1,
                DataLabels = true,
                LabelPoint = point => point.Y + "%"
            });

            LabelsMeses = meses;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Inicio_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).FramePrincipal.Navigate(this);
        }

        private void MenuTurma_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).FramePrincipal.Navigate(new TurmaView());
        }

        private void ProcurarTurma_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).FramePrincipal.Navigate(new ProcurarView());
        }
    }
}