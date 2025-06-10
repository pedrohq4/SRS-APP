using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Faculdade.APP.Model.Entities
{
    public class Admin : Pessoa
    {
        public int IdFuncionario { get; set; }
        public Admin(string nome, string sobrenome, string cpf, int idFuncionario) : base(nome, sobrenome, cpf)
        {
            IdFuncionario = idFuncionario;
        }

        public override string FormatarParaString()
        {
            throw new NotImplementedException();
        }
    }
}
