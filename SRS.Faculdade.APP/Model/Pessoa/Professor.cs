using SRS.Faculdade.APP.Model.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Faculdade.Model
{
    internal class Professor : Pessoa
    {
        public string Titulo {  get; private set; }
        public string Departamento { get; private set; }
        public Dictionary<string, Turma> TurmasLecionadas {  get; private set; }

        public Professor(string nome, string cpf, string titulo, string departamento) : base(nome, cpf)
        {
            Titulo = titulo;
            Departamento = departamento;
            TurmasLecionadas = new Dictionary<string, Turma>();
        }

        public void AdcionarTurma(Turma turma)
        {
            TurmasLecionadas.Add(turma.Nome, turma);
        }

        public void RemoverTurma(string nomeDaTurma)
        {
            TurmasLecionadas.Remove(nomeDaTurma);
        }

        public override string GetDadosAdcionais()
        {
            return $"Titulo: {Titulo}\nDepartamento: {Departamento}";
        }

        public string GetTodasTurmas()
        {
            foreach (var turma in TurmasLecionadas)
            {
                return "";
            }

            return "";
        }

        public Turma GetDaTurma(string nomeTurma)
        {
            return TurmasLecionadas[nomeTurma];
        }
    }
}
