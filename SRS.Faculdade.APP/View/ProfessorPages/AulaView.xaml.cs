using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using SRS.Faculdade.APP.Model.Academico;
using SRS.Faculdade.APP.Model.Entities;
using static SRS.Faculdade.APP.MainWindow;

namespace SRS.Faculdade.APP.View.ProfessorPages
{
    public partial class AulaView : Page
    {
        private List<Aula> _aulasMockadas;
        private Professor Professor;

        public AulaView()
        {
            InitializeComponent();
            Professor = AppState.ProfessorLogado;
            CarregarAulasMockadas();
            ExibirAulas();
        }

        private void CarregarAulasMockadas()
        {
            _aulasMockadas = new List<Aula>();
            Curso disciplinaMock = new Curso("Programação Orientada a Objetos", "POO202", 40);

            Turma turmaMock = new Turma(201, DayOfWeek.Tuesday, "Lab 5", 25, 40, disciplinaMock, Professor);

            Estudante aluno1 = new Estudante("Lucas", "Pereira", "666.666.666-66", "Graduação", "Ciência da Computação");
            Estudante aluno2 = new Estudante("Gabriela", "Lima", "777.777.777-77", "Graduação", "Ciência da Computação");
            Estudante aluno3 = new Estudante("Rafael", "Santos", "888.888.888-88", "Graduação", "Ciência da Computação");
            Estudante aluno4 = new Estudante("Isabela", "Oliveira", "999.999.999-99", "Graduação", "Ciência da Computação");
            Estudante aluno5 = new Estudante("Fernando", "Rocha", "101.101.101-01", "Graduação", "Ciência da Computação");

            turmaMock.Incricao(aluno1);
            turmaMock.Incricao(aluno2);
            turmaMock.Incricao(aluno3);
            turmaMock.Incricao(aluno4);
            turmaMock.Incricao(aluno5);

            Aula aula1 = new Aula
            {
                Data = new DateTime(2025, 3, 10),
                Topico = "Introdução ao WPF",
                TurmaAssociada = turmaMock,
                Presencas = new Dictionary<Estudante, bool>
                {
                    { aluno1, true },
                    { aluno2, true },
                    { aluno3, false },
                    { aluno4, true },
                    { aluno5, false }
                }
            };
            _aulasMockadas.Add(aula1);

            
            Aula aula2 = new Aula
            {
                Data = new DateTime(2025, 3, 17),
                Topico = "Data Binding em WPF",
                TurmaAssociada = turmaMock,
                Presencas = new Dictionary<Estudante, bool>
                {
                    { aluno1, true },
                    { aluno2, false },
                    { aluno3, true },
                    { aluno4, true },
                    { aluno5, true }
                }
            };
            _aulasMockadas.Add(aula2);

            
            Aula aula3 = new Aula
            {
                Data = new DateTime(2025, 3, 24),
                Topico = "Estilos e Templates",
                TurmaAssociada = turmaMock,
                Presencas = new Dictionary<Estudante, bool>
                {
                    { aluno1, false },
                    { aluno2, true },
                    { aluno3, true },
                    { aluno4, false },
                    { aluno5, true }
                }
            };
            _aulasMockadas.Add(aula3);
        }

        private void ExibirAulas()
        {
            wpAulas.Children.Clear();

            foreach (var aula in _aulasMockadas)
            {
                Card aulaCard = new Card
                {
                    Width = 300,
                    Height = 150,
                    Margin = new Thickness(10),
                    UniformCornerRadius = 8,
                    Background = (Brush)new BrushConverter().ConvertFromString("#E0F2F7")
                };

                
                StackPanel cardContent = new StackPanel { Margin = new Thickness(15) };
                cardContent.Children.Add(new TextBlock
                {
                    Text = $"Data: {aula.Data.ToShortDateString()}",
                    Style = (Style)FindResource("MaterialDesignSubtitle1TextBlock"),
                    Foreground = (Brush)new BrushConverter().ConvertFromString("#263238")
                });
                cardContent.Children.Add(new TextBlock
                {
                    Text = $"Tópico: {aula.Topico}",
                    Style = (Style)FindResource("MaterialDesignSubtitle2TextBlock"),
                    Foreground = (Brush)new BrushConverter().ConvertFromString("#455A64")
                });
                cardContent.Children.Add(new TextBlock
                {
                    Text = $"Presentes: {aula.Presencas.Count(p => p.Value)} / {aula.Presencas.Count}",
                    Style = (Style)FindResource("MaterialDesignCaptionTextBlock"),
                    Foreground = (Brush)new BrushConverter().ConvertFromString("#607D8B")
                });

                
                Button btnDetalhes = new Button
                {
                    Content = "Ver Detalhes",
                    Style = (Style)FindResource("MaterialDesignOutlinedButton"),
                    Margin = new Thickness(0, 10, 0, 0),
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Tag = aula
                };
                btnDetalhes.Click += BtnDetalhes_Click;

                cardContent.Children.Add(btnDetalhes);
                aulaCard.Content = cardContent;
                wpAulas.Children.Add(aulaCard);
            }
        }

        private void BtnDetalhes_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                Aula aulaSelecionada = clickedButton.Tag as Aula;
                if (aulaSelecionada != null)
                {
                    
                    AulaDetalhesWindow detalhesWindow = new AulaDetalhesWindow(aulaSelecionada);
                    detalhesWindow.ShowDialog();

                    
                    ExibirAulas();
                }
            }
        }
    }

    
    public class Aula
    {
        public DateTime Data { get; set; }
        public string Topico { get; set; }
        public Turma TurmaAssociada { get; set; }
        public Dictionary<Estudante, bool> Presencas { get; set; }
    }
}


