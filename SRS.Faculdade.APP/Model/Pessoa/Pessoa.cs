using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Faculdade.Model
{
    public abstract class Pessoa
    {
        protected Pessoa(string nome, string cpf)
        {
            Nome = nome;
            Cpf = cpf;
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do aluno deve ser preenchido", AllowEmptyStrings = false)]
        public string Nome { get; private set; }

        [Required(ErrorMessage = "O cpf do aluno deve ser preenchido", AllowEmptyStrings = false)]
        [StringLength(11)]
        public string Cpf { get; private set; }

        public abstract string GetDadosAdcionais();

        public string DadosToString()
        {
            return $"Informações pessoais:\nNome: {Nome}\nCpf: {Cpf}\n{GetDadosAdcionais()}";
        }
    }
}
