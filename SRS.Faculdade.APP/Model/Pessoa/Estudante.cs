using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Faculdade.Model
{
    class Estudante : Pessoa
    {
        private string Curso { get; set; }                 
        private string Titulo { get; set; }         
        private HistoricoEscolar Historico {  get; set; }
        public List<Turma> TurmasMatriculadas { get; private set; }

        public Estudante(string nome, string cpf, string curso, string titulo) : base(nome, cpf)
        {
            Curso = curso;
            Titulo = titulo;
            Historico = new HistoricoEscolar;
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
            var cursoDaNovaTurma = novaTurma.CursoRepresentado;

            foreach(var turma in TurmasMatriculadas)
            {
                var cursoAtual = turma.CursoRepesentado;

                if(cursoAtual == cursoDaNovaTurma && turma.Nota(this) == null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
