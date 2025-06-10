using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRS.Faculdade.APP.Model.Entities;

namespace SRS.Faculdade.APP.Model.Academico
{
    public class Curso
    {
        public string CodigoDisciplina {  get; set; }   
        public string NomeDisciplina { get; set;}       
        public double CargaHoraria { get; set; }

        public Curso(string codigoDisciplina, string nomeDisciplina, double cargaHoraria)
        {
            CodigoDisciplina = codigoDisciplina;
            NomeDisciplina = nomeDisciplina;
            CargaHoraria = cargaHoraria;
        }
    }
}