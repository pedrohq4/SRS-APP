using SRS.Faculdade.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SRS.Faculdade.APP.Model.Academico
{
    internal class HistoricoAcademico
    {
        private List<HistoricoTurma> Historico;
        public Estudante Estudante {  get; set; }

        public HistoricoAcademico(Estudante estudante)
        {
            Estudante = estudante;

            Historico = new List<HistoricoTurma>();
        }

        public bool FoiConcluido(Curso curso)
        {
            return Historico.Where(hs => hs.Turma.DisciplinaDoCurso == curso).Any(te => HistoricoTurma.FoiAprovado(te.Nota));
        }

        public void AdcionarHistoricoTurma(HistoricoTurma historico)
        {
            Historico.Add(historico);
        }
    }
}
