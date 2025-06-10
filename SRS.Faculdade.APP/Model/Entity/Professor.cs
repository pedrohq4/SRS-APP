using SRS.Faculdade.APP.Model.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Faculdade.APP.Model.Entities
{
    public class Professor : Pessoa
    {
        public int IdFuncionario { get; set; }
        public string Titulo {  get; set; }
        public string Departamento { get; set; }
        public Dictionary<string, Turma> TurmasLecionadas {  get; set; }
        public string Formatado { get; set; }

        public Professor(int idFuncionario, string nome,string sobrenome , string cpf, string titulo, string departamento) : base(nome, sobrenome, cpf)
        {
            IdFuncionario = idFuncionario;
            Titulo = titulo;
            Departamento = departamento;
            TurmasLecionadas = new Dictionary<string, Turma>();
            Formatado = FormatarParaString();
        }

        public void AdcionarTurma(Turma turma)
        {
            turma.Incricao(this);
        }

        public void RemoverTurma(string nomeDaTurma)
        {
            TurmasLecionadas.Remove(nomeDaTurma);
        }

        public override string FormatarParaString()
        {
            return $"Professor: {Nome} {SobreNome}";
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
