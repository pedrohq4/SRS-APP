using SRS.Faculdade.APP.Model.Entities;
using System.Text;

namespace SRS.Faculdade.APP.Model.Academico
{
    public class Turma
    {
        public string Nome => DisciplinaDoCurso.CodigoDisciplina + "-" + Numero;
        public int Numero {  get; set; }              
        public DayOfWeek DiaSemana { get; set; }    
        public string Horario { get; set; }
        public string Sala {  get; set; }         
        public Curso DisciplinaDoCurso {  get; set; }      
        public Professor Professor {  get; set; }
        public Dictionary<string, Estudante> EstudantesInscritos { get; set; }
        public int CapacidadeAlunos { get; set; }
        public Dictionary<Estudante, HistoricoTurma> RegistoEstudante { get; set; }
        public int Presenca {  get; set; }
        public int TotalAulas { get; set; }
        public string Formatado => FormatarParaString();
        public string FormatadoBasico => FormatarParaStringBasico();

        public Turma(int numero, DayOfWeek diaSemana, string horario, string sala, int capacidadeAlunos, int TotalAulas, Curso disciplinaAssociada, Professor professor)
        {
            Numero = numero;
            DiaSemana = diaSemana;
            Horario = horario;
            Sala = sala;
            DisciplinaDoCurso = disciplinaAssociada;
            Professor = professor;
            CapacidadeAlunos = capacidadeAlunos;

            EstudantesInscritos = new Dictionary<string, Estudante>(capacidadeAlunos);
            RegistoEstudante = new Dictionary<Estudante, HistoricoTurma>(capacidadeAlunos);
        }

        public void Incricao(Estudante estudante)
        {
            if (EstudantesInscritos.Count >= CapacidadeAlunos)
            {
                throw new InvalidOperationException("A turma está cheia. Não é possível realizar a inscrição.");
            }
            else if (EstudantesInscritos.ContainsKey(estudante.Cpf))
            {
                throw new InvalidOperationException("Você já está inscrito nesta turma.");
            }

            estudante.TurmasMatriculadas.Add(this);
            EstudantesInscritos.Add(estudante.Cpf, estudante);
            RegistoEstudante.Add(estudante, new HistoricoTurma(estudante, this));
        }

        public void Incricao(Professor professor)
        {
            if (EstudantesInscritos.Count >= CapacidadeAlunos)
            {
                throw new InvalidOperationException("A turma já tem um professor");
            }
            else if (EstudantesInscritos.ContainsKey(professor.Cpf))
            {
                throw new InvalidOperationException("Ja leciona essa turma");
            }

            professor.TurmasLecionadas.Add(Nome ,this);
            Professor = professor;
        }

        public bool LancarNota(Estudante estudante, string nota, int numeroNota)
        {
            if (!HistoricoTurma.EhNotaValida(nota))
            {
                throw new InvalidOperationException("Nota em padrão errado!");
            }

            if (!RegistoEstudante.ContainsKey(estudante))
            {
                throw new InvalidOperationException("Estudante não encontrado nesta turma.");
            }

            var historico = RegistoEstudante[estudante];

            switch (numeroNota)
            {
                case 1:
                    historico.Nota1 = nota;
                    break;
                case 2:
                    historico.Nota2 = nota;
                    break;
                case 3:
                    historico.Nota3 = nota;
                    break;
                case 4:
                    historico.Nota4 = nota;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("numeroNota", "O número da nota deve ser entre 1 e 4.");
            }
            return true;
        }

        public string ObterDesempenho(Estudante estudante)
        {
            if (RegistoEstudante.TryGetValue(estudante, out HistoricoTurma historico))
            {
                return historico.CalcularMediaFinal();
            }
            throw new InvalidOperationException("Estudante não encontrado nesta turma.");
        }

        public bool EstaAprovado(Estudante estudante)
        {
            if (RegistoEstudante.TryGetValue(estudante, out HistoricoTurma historico))
            {
                return historico.EstaAprovado();
            }
            throw new InvalidOperationException("Estudante não encontrado nesta turma.");
        }

        private string FormatarParaString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Turma: {Nome}");
            sb.AppendLine($"Disciplina: {DisciplinaDoCurso?.NomeDisciplina ?? "N/A"}");
            sb.AppendLine($"Dia da semana: {DiaSemana}");
            sb.AppendLine($"Horário: {Horario ?? "N/A"}");
            sb.AppendLine($"Sala: {Sala ?? "N/A"}");
            sb.AppendLine($"Capacidade: {CapacidadeAlunos} estudantes");
            sb.AppendLine($"Professor: {Professor?.Nome ?? "N/A"}");

            return sb.ToString();
        }

        public string FormatarParaStringBasico()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Turma: {Nome}");
            sb.AppendLine($"Disciplina: {DisciplinaDoCurso?.NomeDisciplina ?? "N/A"} (Código: {DisciplinaDoCurso?.CodigoDisciplina ?? "N/A"})");
            sb.AppendLine($"Dia da semana: {DiaSemana}");
            sb.AppendLine($"Professor: {Professor?.Nome ?? "N/A"}");

            return sb.ToString();
        }

        public int ContarTotalAulas()
        {
            return TotalAulas;
        }
    }
}


