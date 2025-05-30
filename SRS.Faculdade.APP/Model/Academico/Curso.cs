using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Faculdade.APP.Model.Academico
{
    internal class Curso
    {
        private string CodigoDisciplina {  get; set; }   
        private string NomeDisciplina { get; set;}       
        private double CargaHoraria { get; set; }
        private List<Turma> TurmasOfertadas { get; set; }
        private List<Disciplina> PreRequisitos { get; set; }

        public Curso(string codigoDisciplina, string nomeDisciplina, double cargaHoraria)
        {

        }
    }
}