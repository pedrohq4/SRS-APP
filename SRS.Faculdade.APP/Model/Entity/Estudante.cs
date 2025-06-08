using SRS.Faculdade.APP.Model.Academico;

namespace SRS.Faculdade.APP.Model.Entities
{
    public class Estudante : Pessoa
    {
        public string Curso { get; private set; }                 
        public string Titulo { get; private set; }         
        public HistoricoAcademico Historico { get; private set; }
        public List<Turma> TurmasMatriculadas { get; private set; }

        public Estudante(string nome, string sobrenome, string cpf, string titulo, string curso) : base(nome, sobrenome, cpf)
        {
            Curso = curso;
            Titulo = titulo;
            Historico = new HistoricoAcademico(this);
            TurmasMatriculadas = new List<Turma>();
        }

        public override string GetDadosAdcionais()
        {
            throw new NotImplementedException();
        }

        public void AdcionarTurma(Turma turma)
        {
            TurmasMatriculadas.Add(turma);
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
