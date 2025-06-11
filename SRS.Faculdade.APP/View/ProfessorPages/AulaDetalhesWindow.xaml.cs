using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using SRS.Faculdade.APP.Model.Academico;
using SRS.Faculdade.APP.Model.Entities;

namespace SRS.Faculdade.APP.View.ProfessorPages
{
    public partial class AulaDetalhesWindow : Window
    {
        private Aula _aulaAtual;
        private List<EstudantePresencaWrapper> _listaDeEstudantesPresenca;

        public AulaDetalhesWindow(Aula aula)
        {
            InitializeComponent();
            _aulaAtual = aula;
            CarregarDetalhesAula();
            CarregarListaAlunosPresenca();
        }

        private void CarregarDetalhesAula()
        {
            txtAulaData.Text = $"Data: {_aulaAtual.Horario.ToString()}";
            txtAulaTopico.Text = $"Tópico: {_aulaAtual.Topico}";
            txtAulaTurma.Text = $"Turma: {_aulaAtual.TurmaAssociada.Nome}";
        }

        private void CarregarListaAlunosPresenca()
        {
            _listaDeEstudantesPresenca = _aulaAtual.TurmaAssociada.EstudantesInscritos.Values.Select(estudante => new EstudantePresencaWrapper
            {
                Estudante = estudante,
                IsPresente = _aulaAtual.Presencas.TryGetValue(estudante, out var presente) && presente

            }).ToList();

            dgAlunosPresenca.ItemsSource = _listaDeEstudantesPresenca;

        }

        private async void SalvarPresencas_Click(object sender, RoutedEventArgs e)
        {
            var dialogContent = new StackPanel
            {
                Margin = new Thickness(20),
                Children =
                {
                    new TextBlock
                    {
                        Text = "Deseja realmente salvar as presenças?",
                        Margin = new Thickness(0, 0, 0, 20)
                    },
                    new StackPanel
                    {
                        Orientation = Orientation.Horizontal,
                        HorizontalAlignment = HorizontalAlignment.Right,
                        Children =
                        {
                            new Button
                            {
                                Content = "Cancelar",
                                Margin = new Thickness(0, 0, 10, 0),
                                Command = DialogHost.CloseDialogCommand,
                                CommandParameter = false
                            },
                            new Button
                            {
                                Content = "Salvar",
                                Margin = new Thickness(0, 0, 10, 0),
                                Command = DialogHost.CloseDialogCommand,
                                CommandParameter = true
                            }
                        }
                    }
                }
            };

            var result = await DialogHost.Show(dialogContent, "RootDialogHost");

            if (result is bool boolResult && boolResult)
            {
                foreach (var item in _listaDeEstudantesPresenca)
                {
                    _aulaAtual.Presencas[item.Estudante] = item.IsPresente;
                }

                MessageBox.Show("Presenças salvas com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter is bool parameter && parameter == false)
            {
                eventArgs.Cancel(); 
            }
        }
    }
    public class EstudantePresencaWrapper : INotifyPropertyChanged
    {
        private bool _isPresente;

        public Estudante Estudante { get; set; }

        public bool IsPresente
        {
            get => _isPresente;
            set
            {
                if (_isPresente != value)
                {
                    _isPresente = value;
                    Debug.WriteLine($"Presença alterada para: {value} - Estudante: {Estudante.Cpf}");
                    OnPropertyChanged();
                }
            }
        }

        public string NomeCompleto => $"{Estudante.Nome} {Estudante.SobreNome}";

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}


