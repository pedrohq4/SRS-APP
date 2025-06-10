using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Faculdade.APP.Model.Entities
{
    public abstract class Pessoa
    {
        public string Nome { get; private set; }
        public string SobreNome { get; private set; }
        public int Id { get; set; }
        public string Cpf { get; private set; }
        public Usuario Usuario { get; set; }

        protected Pessoa(string nome, string sobrenome, string cpf)
        {
            Nome = nome;
            Cpf = cpf;
            SobreNome = sobrenome;

            if (this is Estudante)
            {
                Usuario = new Usuario(TipoUsuario.Estudante, Nome, sobrenome);
            }
            else if (this is Professor)
            {
                Usuario = new Usuario(TipoUsuario.Professor, Nome, sobrenome);
            }
            else if (this is Admin)
            {
                Usuario = new Usuario(TipoUsuario.Admin, Nome, sobrenome);
            }
        }

        public abstract string FormatarParaString();

        public string DadosToString()
        {
            return $"Informações pessoais:\nNome: {Nome}\nCpf: {Cpf}\n{FormatarParaString()}";
        }
    }
}
