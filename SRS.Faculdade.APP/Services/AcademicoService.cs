using SRS.Faculdade.APP.Model.Academico;
using SRS.Faculdade.APP.Model.Entities;

namespace SRS.Faculdade.APP.Services
{
    public class AcademicoService
    {

        private IList<Turma> _Turmas;
        private IList<Curso> _Cursos;

        public AcademicoService()
        {
            CarregarListaInicial();

        }

        private void CarregarListaInicial()
        {
            var professor = new Professor(101, "João", "Cuz", "23", "Baicharel", "Humanas");

            _Turmas = new List<Turma>()
            {
                new Turma(201, DayOfWeek.Thursday,"302",32, 20,new Curso("MT", "Matematica", 30), professor),
                new Turma(202, DayOfWeek.Monday, "205", 25, 20, new Curso("FS", "Física", 25), professor),
                new Turma(203, DayOfWeek.Tuesday, "110", 30, 20, new Curso("HT", "História", 30), professor),
                new Turma(204, DayOfWeek.Wednesday, "Lab3", 20, 20, new Curso("QM", "Química", 20), professor),
                new Turma(205, DayOfWeek.Friday, "215", 28, 20, new Curso("LT", "Literatura", 28), professor),
                new Turma(206, DayOfWeek.Thursday, "Lab2", 22, 20, new Curso("BL", "Biologia", 22), professor)
            };

            _Cursos = new List<Curso>
            {
                new Curso("MT", "Matematica", 60),
                new Curso("FI", "Fisica", 60),
                new Curso("HI", "Historia", 40),
                new Curso("QU", "Quimica", 60),
                new Curso("LI", "Literatura", 40),
                new Curso("BI", "Biologia", 60)
            };
        }

        public IList<Turma> ObterTodasTurmas() => _Turmas;
        public IList<Curso> ObterTodosCursos() => _Cursos;
        public void CriarTurma(int numero, DayOfWeek diaSemana, string horario, string sala, int capacidadeAlunos, int TotalAulas, Curso disciplinaAssociada, Professor professor)
        {
            foreach(var turma in _Turmas)
            {
                if (turma.Numero == numero)
                {
                    throw new Exception("Turma ja existente");
                }
            }
            
            _Turmas.Add(new Turma(numero, diaSemana, sala, capacidadeAlunos, TotalAulas, disciplinaAssociada, professor));
        }

    }
}
