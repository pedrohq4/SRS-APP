using SRS.Faculdade.Pessoa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Faculdade.Model
{
    internal class Professor : Pessoa
    {
        private string Titulo {  get; set; }
        private string Departamento { get; set; }
        private List<Section> TurmasAtuais {  get; set; }

        public Professor(string nome, string cpf, string titulo, string departamento) : base(nome, cpf)
        {
            Titulo = titulo;
            Departamento = departamento;

            Teaches = new List<Section>();
        }

        public override string GetDadosAdcionais()
        {
            return $"Titulo: {Titulo}\nDepartamento: {Departamento}";
        }

        public string DadosTodasTurmas()
        {
            foreach (var turma in TurmasAtuais)
            {
                return "";
            }

            return "";
        }

        public string DadosTurma(int id)
        {

        }
    }
}
