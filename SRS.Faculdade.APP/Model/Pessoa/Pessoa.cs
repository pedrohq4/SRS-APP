using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Faculdade.APP.Model.Pessoa
{
    public abstract class Pessoa
    {
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public int Id { get; set; }
        public string Cpf { get; private set; }

        protected Pessoa(string nome, string sobrenome, string cpf)
        {
            Nome = nome;
            Cpf = cpf;
            Sobrenome = sobrenome;
        }

        public abstract string GetDadosAdcionais();

        public string DadosToString()
        {
            return $"Informações pessoais:\nNome: {Nome}\nCpf: {Cpf}\n{GetDadosAdcionais()}";
        }
    }
}
