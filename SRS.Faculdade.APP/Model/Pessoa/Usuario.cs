using SRS.Faculdade.APP.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Faculdade.APP.Model.Pessoa
{
    public class Usuario
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool EhAdmin { get; set; }
        public Pessoa Pessoa { get; set; }

        public Usuario(TipoUsuario tipoUsuario,string nome, string sobrenome, string cpf, string titulo, string materia)
        {
            EhAdmin = false;
            Senha = cpf;

            if (tipoUsuario is TipoUsuario.Professor)
            {
                Pessoa = new Professor(nome, sobrenome, cpf, titulo, materia);
                Email = GerarEmail(nome, sobrenome,tipoUsuario);
                
            }
            else if(tipoUsuario is TipoUsuario.Aluno)
            {
                Pessoa = new Estudante(nome, sobrenome, cpf, titulo, materia);
                Email = GerarEmail(nome, sobrenome, tipoUsuario);
            }
        }

        public string GerarEmail(string nome, string sobrenome, TipoUsuario tipoUsuario)
        {
            string sobrenomeFormatado;

            if (sobrenome.Contains(' '))
            {
                var sobrenomes = sobrenome.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                sobrenomeFormatado = sobrenomes[0] + string.Concat(sobrenomes.Skip(1));
            }
            else
            {
                sobrenomeFormatado = sobrenome;
            }

            if(tipoUsuario is TipoUsuario.Aluno)
                return $"{nome.ToLower()}.{sobrenomeFormatado.ToLower()}@Aluno.edu";

            else if(tipoUsuario is TipoUsuario.Professor)
                return $"{nome.ToLower()}.{sobrenomeFormatado.ToLower()}@Professor.edu";

            return string.Empty;
        }
    }
}
