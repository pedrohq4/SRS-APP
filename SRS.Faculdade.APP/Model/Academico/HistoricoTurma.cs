using SRS.Faculdade.APP.Model.Pessoa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SRS.Faculdade.APP.Model.Academico
{
    public class HistoricoTurma
    {
        public string Nota {  get; set; }
        public Estudante Estudante { get; set; }
        public Turma Turma { get; set; }
        public HistoricoAcademico Historico {  get; set; }

        public HistoricoTurma(string nota, Estudante estudante, Turma turma)
        {
            HistoricoAcademico historico = estudante.Historico;
            Historico = historico;
            historico.AdcionarHistoricoTurma(this);
        }

        public static bool EhNotaValida(string nota)
        {
            bool valido = false;

            if (nota.Length >= 3 && nota.Length < 0 && nota.StartsWith("D"))
                valido = true;
            
            else
                valido = false;

            return valido;
        }

        public static bool FoiAprovado(string nota)
        {
            bool aprovado = false;

            if(nota.Equals("D") || nota.Equals("DL")|| nota.Equals("DML"))
                return aprovado = true;

            return aprovado;
        }
    }
}
