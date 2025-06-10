using SRS.Faculdade.APP.Model.Academico;
using SRS.Faculdade.APP.Model.Entities;
using SRS.Faculdade.APP.Services;
using System;
using System.Windows;
using System.Windows.Controls;
using static SRS.Faculdade.APP.MainWindow;

namespace SRS.Faculdade.APP.View.AdminPages
{
    public partial class AdminView : Page
    {
        public AcademicoService _AcademicoService;
        public UsuarioService _UsuarioService;

        public AdminView()
        {
            InitializeComponent();
            if (panelTurma != null && panelPessoa != null)
            {
                panelTurma.Visibility = Visibility.Visible;
                panelPessoa.Visibility = Visibility.Collapsed;
            }
            
            _AcademicoService = AppState.AcademicoService;
            _UsuarioService = AppState.UsuarioService;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (panelTurma == null || panelPessoa == null) return;

            if (rbTurma.IsChecked == true)
            {
                panelTurma.Visibility = Visibility.Visible;
                panelPessoa.Visibility = Visibility.Collapsed;
            }
            else if (rbPessoa.IsChecked == true)
            {
                panelTurma.Visibility = Visibility.Collapsed;
                panelPessoa.Visibility = Visibility.Visible;
            }
        }

        private void Salvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool isValid = false;

                if (rbTurma.IsChecked == true)
                {
                    isValid = ValidateTurmaFields();
                    if (isValid)
                    {
                        var curso = txtCursoDaTurma.Text;
                        var diaSemanaTexto = (cmbDiaSemana.SelectedItem as ComboBoxItem)?.Content?.ToString();
                        Enum.TryParse<DayOfWeek>(diaSemanaTexto, out DayOfWeek diaSemana);
                        var horario = txtHorarioInicio.Text;
                        var salaTurma = txtNumeroSala.Text;

                        if (!int.TryParse(txtProfessor.Text, out int professorId))
                        {
                            throw new InvalidOperationException("Id invalido. Digite um número inteiro.");
                        }

                        if (!int.TryParse(txtCodigoTurma.Text, out int codigoTurma))
                        {
                            throw new InvalidOperationException("Codigo da turma invalido. Digite um número inteiro.");
                        }

                        if (!int.TryParse(txtCapacidadeAlunos.Text, out int capacidadeAlunos))
                        {
                            throw new InvalidOperationException("Capacidade de alunos inválida. Digite um número inteiro.");
                        }

                        if (!int.TryParse(txtNumeroAulas.Text, out int totalAulas))
                        {
                            throw new InvalidOperationException("Numeros de aula inválido. Digite um número inteiro.");
                        }

                        if (!CursoExiste(curso))
                        {
                            throw new InvalidOperationException("Curso inválido. Insira um curso válido.");
                        }

                        Curso cursoFormatado = AcharCurso(curso);

                        var professorDaTurma = _UsuarioService.ObterTodos().OfType<Professor>().FirstOrDefault(p => p.IdFuncionario == professorId);

                        if (professorDaTurma == null)
                        {
                            throw new InvalidOperationException("Professor não encontrado.");
                        }

                        _AcademicoService.CriarTurma(codigoTurma, diaSemana, horario, salaTurma, capacidadeAlunos, totalAulas, cursoFormatado, professorDaTurma);

                        MessageBox.Show("Dados da Turma salvos com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }

                else if (rbPessoa.IsChecked == true)
                {
                    isValid = ValidatePessoaFields();
                    if (isValid)
                    {
                        var nome = txtNome.Text;
                        var sobrenome = txtSobreNome.Text;
                        var Cpf = txtCPF.Text;
                        var tipoPessoaTexto = (cmbTipoPessoa.SelectedItem as ComboBoxItem)?.Content?.ToString();

                        if (!Enum.TryParse<TipoUsuario>(tipoPessoaTexto, ignoreCase: true, out TipoUsuario tipoUsuario))
                        {
                            throw new InvalidOperationException("Tipo de usuário inválido.");
                        }

                        foreach(var pessoa in _UsuarioService.ObterTodos())
                        {
                            if(pessoa.Cpf == Cpf)
                            {
                                throw new InvalidOperationException("Usuario ja existe em nosso sistema");
                            }
                        }

                        var tipoDeGraduacao = txtTipoGraduacao.Text;
                        var curso = txtCursoDoEstudante.Text;

                        _UsuarioService.AdcionarPessoa(tipoUsuario , nome, sobrenome, Cpf, tipoDeGraduacao, curso);
                        MessageBox.Show("Dados da Pessoa salvos com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }

                if (!isValid)
                {
                    throw new InvalidOperationException("Por favor, preencha todos os campos obrigatórios.");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private Curso AcharCurso(string cursoStr)
        {
            Curso cursoCerto = null;
            foreach (var curso in _AcademicoService.ObterTodosCursos())
            {
                if (curso.NomeDisciplina.Equals(cursoStr, StringComparison.OrdinalIgnoreCase))
                {
                    cursoCerto = curso;
                    break;
                }
            }
            return cursoCerto;
        }

        private bool CursoExiste(string cursoTexto)
        {
            return Enum.TryParse(typeof(Cursos), cursoTexto, true, out _);
        }

        private bool ValidateTurmaFields()
        {
            bool allFieldsValid = true;

            if (string.IsNullOrWhiteSpace(txtCodigoTurma.Text))
            {
                allFieldsValid = false;
            }
            if (cmbDiaSemana.SelectedItem == null)
            {
                allFieldsValid = false;
            }
            if (string.IsNullOrWhiteSpace(txtHorarioInicio.Text))
            {
                allFieldsValid = false;
            }
            if (string.IsNullOrWhiteSpace(txtNumeroSala.Text))
            {
                allFieldsValid = false;
            }
            if (string.IsNullOrWhiteSpace(txtCapacidadeAlunos.Text))
            {
                allFieldsValid = false;
            }
            if (string.IsNullOrWhiteSpace(txtNumeroAulas.Text))
            {
                allFieldsValid = false;
            }
            if (string.IsNullOrWhiteSpace(txtCursoDaTurma.Text))
            {
                allFieldsValid = false;
            }
            if (string.IsNullOrWhiteSpace(txtProfessor.Text))
            {
                allFieldsValid = false;
            }

            return allFieldsValid;
        }

        private bool ValidatePessoaFields()
        {
            bool allFieldsValid = true;

            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                allFieldsValid = false;
            }
            if (string.IsNullOrWhiteSpace(txtCPF.Text))
            {
                allFieldsValid = false;
            }
            if (string.IsNullOrWhiteSpace(txtCursoDoEstudante.Text))
            {
                allFieldsValid = false;
            }
            if (string.IsNullOrWhiteSpace(txtTipoGraduacao.Text))
            {
                allFieldsValid = false;
            }
            if (cmbTipoPessoa.SelectedItem == null)
            {
                allFieldsValid = false;
            }

            return allFieldsValid;
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Operação de inclusão cancelada.", "Cancelado", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Inicio_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).FramePrincipal.Navigate(this);
        }

        private void ProcurarTurma_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).FramePrincipal.Navigate(new ProcurarView());
        }
    }
}


