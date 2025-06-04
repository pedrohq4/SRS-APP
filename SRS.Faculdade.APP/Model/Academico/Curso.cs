using SRS.Faculdade.APP.Model.Pessoa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Faculdade.APP.Model.Academico
{
    internal class Curso
    {
        public string CodigoDisciplina {  get; set; }   
        public string NomeDisciplina { get; set;}       
        public double CargaHoraria { get; set; }
        public List<Turma> TurmasOfertadas { get; set; }
        public List<Curso> PreRequisitos { get; set; }

        public Curso(string codigoDisciplina, string nomeDisciplina, double cargaHoraria)
        {
            CodigoDisciplina = codigoDisciplina;
            NomeDisciplina = nomeDisciplina;
            CargaHoraria = cargaHoraria;

            TurmasOfertadas = new List<Turma>();
            PreRequisitos = new List<Curso>();
        }

        public void AdcionarPreRequesistos(Curso curso)
        {
            PreRequisitos.Add(curso);
        }

        public bool TemPreRequesitos()
        {
            return PreRequisitos.Count < 0;
        }

        public void CriarTurma(int numeroTurma, DayOfWeek dia, string horario ,string duracao, string sala, int capacidade, Professor professor)
        {
            Turma turma = new Turma(numeroTurma, dia, horario, sala, capacidade, this, professor);
            TurmasOfertadas.Add(turma);
        }
    }
}