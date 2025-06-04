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

        public Usuario(bool ehAdmin, TipoUsuario tipoUsuario,string nome, string sobrenome, string cpf, string titulo, string materia)
        {
            Senha = cpf;
            EhAdmin = ehAdmin;

            if (tipoUsuario is TipoUsuario.Professor)
            {
                Pessoa = new Professor(nome, sobrenome, cpf, titulo, materia);
                
            }
            else if(tipoUsuario is TipoUsuario.Aluno)
            {
                Pessoa = new Estudante(nome, sobrenome, cpf, titulo, materia);
            }
        }

        public string GerarEmail(string nome, string sobrenome)
        {
            
        }
    }
}
