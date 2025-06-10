using SRS.Faculdade.APP.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SRS.Faculdade.APP.Model.Academico
{
    public class HistoricoAcademico
    {
        public List<HistoricoTurma> Turmas;
        public Estudante Estudante {  get; set; }

        public HistoricoAcademico(Estudante estudante)
        {
            Estudante = estudante;

            Turmas = new List<HistoricoTurma>();
        }

        public bool FoiConcluido(Curso curso)
        {
            var historicos = Turmas.Where(hs => hs.Turma.DisciplinaDoCurso == curso);

            foreach (var historico in historicos)
            {
                var notas = new[] { historico.Nota1, historico.Nota2, historico.Nota3, historico.Nota4 };

                if (notas.Any(nota => nota == null))
                    return false;

                if (notas.Any(nota => HistoricoTurma.FoiAprovado(nota)))
                    return true;
            }

            return false;
        }

        public void AdcionarHistoricoTurma(HistoricoTurma historico)
        {
            Turmas.Add(historico);
        }
    }
}
