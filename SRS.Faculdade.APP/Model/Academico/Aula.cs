using SRS.Faculdade.APP.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Faculdade.APP.Model.Academico
{
    public class Aula
    {
        public TimeOnly Horario { get; set; }
        public string Topico { get; set; }
        public Turma TurmaAssociada { get; set; }
        public Dictionary<Estudante, bool> Presencas { get; set; }

        public Aula(TimeOnly horario, string topico, Turma turma, Dictionary<Estudante, bool> presencas)
        {
            Horario = horario;
            Topico = topico;
            TurmaAssociada = turma;
            Presencas = presencas;
        }
    }
}
