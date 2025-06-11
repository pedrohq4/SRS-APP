using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using SRS.Faculdade.APP.Model.Academico;
using SRS.Faculdade.APP.Model.Entities;

namespace SRS.Faculdade.APP.View.ProfessorPages
{
    /// <summary>
    /// Interaction logic for TurmaDetalhesView.xaml
    /// </summary>
    public partial class DetalheView : Page
    {
        private Turma _turmaAtual;
        private Estudante _alunoSelecionadoParaNota;

        public DetalheView(Turma turma)
        {
            InitializeComponent();

            CarregarDadosMockados(turma);
        }

        private void CarregarDadosMockados(Turma turma)
        {
            _turmaAtual = turma;

            Estudante aluno1 = new Estudante("Maria", "Souza", "111.111.111-11", "Graduação", "Engenharia");
            Estudante aluno2 = new Estudante("Pedro", "Almeida", "222.222.222-22", "Graduação", "Engenharia");
            Estudante aluno3 = new Estudante("Ana", "Costa", "333.333.333-33", "Graduação", "Engenharia");
            Estudante aluno4 = new Estudante("Carlos", "Ferreira", "444.444.444-44", "Graduação", "Engenharia");
            Estudante aluno5 = new Estudante("Juliana", "Martins", "555.555.555-55", "Graduação", "Engenharia");

            _turmaAtual.Incricao(aluno1);
            _turmaAtual.Incricao(aluno2);
            _turmaAtual.Incricao(aluno3);
            _turmaAtual.Incricao(aluno4);
            _turmaAtual.Incricao(aluno5);

            _turmaAtual.LancarNota(aluno1, "DML", 1);
            _turmaAtual.LancarNota(aluno2, "ND", 1);
            _turmaAtual.LancarNota(aluno3, "DM", 1);

            txtNomeTurma.Text = $"Turma: {_turmaAtual.Nome}";
            txtDisciplina.Text = $"Disciplina: {_turmaAtual.DisciplinaDoCurso.NomeDisciplina}";
            txtProfessor.Text = $"Professor: {_turmaAtual.Professor.Nome}";

            AtualizarListaAlunos();
        }

        private void AtualizarListaAlunos()
        {
            var alunosParaExibir = new List<dynamic>();
            foreach (var entry in _turmaAtual.RegistoEstudante)
            {
                Estudante estudante = entry.Key;
                HistoricoTurma historico = entry.Value;

                string notaAtual = "N/A";
                if (!string.IsNullOrEmpty(historico.Nota1))
                {
                    notaAtual = historico.Nota1;
                }

                alunosParaExibir.Add(new
                {
                    NomeCompleto = estudante.Nome + " " + estudante.SobreNome,
                    Matricula = estudante.Cpf,
                    NotaAtual = notaAtual,
                    EstudanteObjeto = estudante
                });
            }
            dgAlunos.ItemsSource = alunosParaExibir;
        }

        private async void LancarNota_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            dynamic alunoData = btn.DataContext;
            _alunoSelecionadoParaNota = alunoData.EstudanteObjeto;

            if (_alunoSelecionadoParaNota == null) return;

            txtAlunoNomeDialog.Text = $"Aluno: {_alunoSelecionadoParaNota.Nome} {_alunoSelecionadoParaNota.SobreNome}";

            
            if (_turmaAtual.RegistoEstudante.TryGetValue(_alunoSelecionadoParaNota, out HistoricoTurma historicoAluno))
            {
                string notaExistente = historicoAluno.Nota1;
                txtNotaAtualDialog.Text = $"Nota Atual: {(string.IsNullOrEmpty(notaExistente) ? "N/A" : notaExistente)}";

                switch (notaExistente?.ToUpper())
                {
                    case "ND": rbND.IsChecked = true; break;
                    case "D": rbD.IsChecked = true; break;
                    case "DM": rbDM.IsChecked = true; break;
                    case "DML": rbDML.IsChecked = true; break;
                    default: 
                        rbND.IsChecked = false;
                        rbD.IsChecked = false;
                        rbDM.IsChecked = false;
                        rbDML.IsChecked = false;
                        break;
                }
            }
            else
            {
                txtNotaAtualDialog.Text = "Nota Atual: N/A";
                rbND.IsChecked = false;
                rbD.IsChecked = false;
                rbDM.IsChecked = false;
                rbDML.IsChecked = false;
            }

            await DialogHost.Show(NotaDialogHost.DialogContent, "RootDialogHost");
        }

        private void SalvarNota_Click(object sender, RoutedEventArgs e)
        {
            if (_alunoSelecionadoParaNota == null) return;

            string novaNota = string.Empty;
            if (rbND.IsChecked == true) novaNota = "ND";
            else if (rbD.IsChecked == true) novaNota = "D";
            else if (rbDM.IsChecked == true) novaNota = "DM";
            else if (rbDML.IsChecked == true) novaNota = "DML";

            if (string.IsNullOrEmpty(novaNota))
            {
                MessageBox.Show("Por favor, selecione uma nota.", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                
                _turmaAtual.LancarNota(_alunoSelecionadoParaNota, novaNota, 1);
                MessageBox.Show("Nota lançada com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                AtualizarListaAlunos();
                DialogHost.CloseDialogCommand.Execute(null, NotaDialogHost);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Voltar_Click(object sender, RoutedEventArgs e)
        {
            var frame = ((MainWindow)Application.Current.MainWindow).FramePrincipal;

            if (frame.CanGoBack)
            {
                frame.GoBack();
            }
        }
    }
}


