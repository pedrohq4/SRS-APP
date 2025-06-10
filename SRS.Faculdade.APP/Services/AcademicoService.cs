using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRS.Faculdade.APP.Model.Academico;
using SRS.Faculdade.APP.Model.Entities;

namespace SRS.Faculdade.APP.Services
{
    public class AcademicoService
    {

        private IList<Turma> _Turmas;

        public AcademicoService()
        {
            if(_Turmas == null)
            _Turmas = new List<Turma>()
            {
                new Turma(201, DayOfWeek.Thursday,"10:30","302",32, 20,new Curso("DR1", "Matematica", 30), new Professor("João", "Silva", "0", "Professor", "Humanas")),
                new Turma(202, DayOfWeek.Monday, "08:00", "205", 25, 20, new Curso("FS1", "Física", 25), new Professor("Maria", "Santos", "1", "Professora", "Exatas")),
                new Turma(203, DayOfWeek.Tuesday, "13:15", "110", 30, 20, new Curso("HT1", "História", 30), new Professor("Carlos", "Oliveira", "2", "Professor", "Humanas")),
                new Turma(204, DayOfWeek.Wednesday, "14:45", "Lab3", 20, 20, new Curso("QM1", "Química", 20), new Professor("Ana", "Costa", "3", "Professora", "Exatas")),
                new Turma(205, DayOfWeek.Friday, "09:30", "215", 28, 20, new Curso("LT1", "Literatura", 28), new Professor("Pedro", "Almeida", "4", "Professor", "Linguagens")),
                new Turma(206, DayOfWeek.Thursday, "16:00", "Lab2", 22, 20, new Curso("BL1", "Biologia", 22), new Professor("Fernanda", "Lima", "5", "Professora", "Ciências"))
            };
        }

        public IList<Turma> ObterTodasTurmas() => _Turmas;
    }
}
