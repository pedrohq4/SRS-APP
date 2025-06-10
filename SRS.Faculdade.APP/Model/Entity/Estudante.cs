using SRS.Faculdade.APP.Model.Academico;

namespace SRS.Faculdade.APP.Model.Entities
{
    public class Estudante : Pessoa
    {
        public string Curso { get; set; }                 
        public string TipoGraduacao { get; set; }         
        public HistoricoAcademico Historico { get; set; }
        public List<Turma> TurmasMatriculadas { get; set; }

        public Estudante(string nome, string sobrenome, string cpf, string tipoGraduacao, string curso) : base(nome, sobrenome, cpf)
        {
            Curso = curso;
            TipoGraduacao = tipoGraduacao;
            Historico = new HistoricoAcademico(this);
            TurmasMatriculadas = new List<Turma>();
        }

        public override string FormatarParaString()
        {
            throw new NotImplementedException();
        }

        public void AdcionarTurma(Turma turma)
        {
            
            turma.Incricao(this);
        }

        public void RemoverTurma(Turma turma)
        {
            TurmasMatriculadas.Remove(turma);
        }

        public bool EstaMatriculadoEm(Turma turma)
        {
            return TurmasMatriculadas.Contains(turma);
        }

        public bool EstaMatriculadoEmDisciplinaSemelhante(Turma novaTurma)
        {
            var cursoDaNovaTurma = novaTurma.DisciplinaDoCurso;

            foreach(var turma in TurmasMatriculadas)
            {
                var curso = turma.DisciplinaDoCurso;

                if(curso == cursoDaNovaTurma)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
