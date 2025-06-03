using SRS.Faculdade.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Faculdade.APP.Model.Academico
{
    internal class Turma
    {
        public string Nome => DisciplinaDoCurso.CodigoDisciplina + " - " + Numero;
        public int Numero {  get; set; }              
        public DayOfWeek DiaSemana { get; set; }    
        public string Horario { get; set; }
        public string Sala {  get; set; }     
        public int CapacidadeEstudantes {  get; set; }       
        public Curso DisciplinaDoCurso {  get; set; }      
        public Professor Professor {  get; set; }
        public Dictionary<string, Estudante> Estudantes { get; set; }
        public Dictionary<Estudante, HistoricoTurma> RegistoEstudante { get; set; }

        public Turma(int numero, DayOfWeek diaSemana, string horario, string sala, int capacidadeAlunos, Curso disciplinaAssociada, Professor professor)
        {
            Numero = numero;
            DiaSemana = diaSemana;
            Horario = horario;
            Sala = sala;
            CapacidadeEstudantes = capacidadeAlunos;
            DisciplinaDoCurso = disciplinaAssociada;
            Professor = professor;

            Estudantes = new Dictionary<string, Estudante>();
            RegistoEstudante = new Dictionary<Estudante, HistoricoTurma>();
        }

        public string Incricao(Estudante estudante)
        {
            HistoricoAcademico historico = estudante.Historico;

            if (estudante.EstaMatriculadoEmDisciplinaSemelhante(this))
            {
                return "Ja inscrito em DisciplinaSemelhante";
            }
            else
            {
                Estudantes.Add(estudante.Nome, estudante);
                return "Inscrito com sucesso";
            }
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
    }
}
