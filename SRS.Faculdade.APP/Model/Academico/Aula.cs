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
        public TimeOnly HorarioDeAula { get; set; }
        public Dictionary<Estudante, bool> Presenca { get; set; }
        
    }
}
