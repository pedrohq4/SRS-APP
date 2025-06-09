using SRS.Faculdade.APP.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Dictionary<string, Estudante> Estudantes { get; set; }
        public int CapacidadeAlunos { get; set; }
        public Dictionary<Estudante, HistoricoTurma> RegistoEstudante { get; set; }
        public int Presenca {  get; set; }
        public int TotalAulas { get; set; }
        public string Formatado => FormatarParaString();

        public Turma(int numero, DayOfWeek diaSemana, string horario, string sala, int capacidadeAlunos, int TotalAulas, Curso disciplinaAssociada, Professor professor)
        {
            Numero = numero;
            DiaSemana = diaSemana;
            Horario = horario;
            Sala = sala;
            DisciplinaDoCurso = disciplinaAssociada;
            Professor = professor;
            CapacidadeAlunos = capacidadeAlunos;

            Estudantes = new Dictionary<string, Estudante>(capacidadeAlunos);
            RegistoEstudante = new Dictionary<Estudante, HistoricoTurma>(capacidadeAlunos);
        }

        public string Incricao(Estudante estudante)
        {
            HistoricoAcademico historico = estudante.Historico;
            if (Estudantes.Count >= CapacidadeAlunos)
            {
                throw new InvalidOperationException("A turma está cheia. Não é possível realizar a inscrição.");
            }

            if (Estudantes.ContainsKey(estudante.Matricula))
            {
                throw new InvalidOperationException("Você já está inscrito nesta turma.");
            }

            Estudantes.Add(estudante.Matricula, estudante);
            RegistoEstudante.Add(estudante, new HistoricoTurma())
        }

        public string LancarNota(Estudante estudante, string nota)
        {
            if (!HistoricoTurma.EhNotaValida(nota))
            {
                return "Nota em padrão errado!";
            }

            if (RegistoEstudante.ContainsKey(estudante))
            {
                return "Nota já lançada para este estudante.";
            }

            var historico = new HistoricoTurma(nota, estudante, this);
            RegistoEstudante[estudante] = historico;

            return "Nota lançada com sucesso.";
        }

        private string FormatarParaString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Turma: {Nome}");
            sb.AppendLine($"Disciplina: {DisciplinaDoCurso.NomeDisciplina}");
            sb.AppendLine($"Dia da semana: {DiaSemana}");
            sb.AppendLine($"Horário: {Horario ?? "N/A"}");
            sb.AppendLine($"Sala: {Sala ?? "N/A"}");
            sb.AppendLine($"Capacidade: {CapacidadeEstudantes} estudantes");
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
    }
}
