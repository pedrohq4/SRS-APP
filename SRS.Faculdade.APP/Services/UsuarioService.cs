using SRS.Faculdade.APP.Model.Entities;

namespace SRS.Faculdade.APP.Services
{
    public class UsuarioService
    {
        private IList<Pessoa> _Pessoas;

        public UsuarioService() => CarregarListaInicial();

        public void CarregarListaInicial()
        {
            _Pessoas = new List<Pessoa>()
            {
                new Admin("João", "bruno", "12", 304), //joao.bruno@Admin.edu
                new Professor(102 , "Jose", "Pereira", "3", "Professor", "Humanas"), //jose.pereira@Professor.edu / 123
                new Professor(193 ,"João", "Silva", "2", "Professor", "Humanas"), //joão.silva@Professor.edu / 123
                new Estudante("Lucas", "Andrade", "0", "Doutorado", "Medicina"), //lucas.andrade@Aluno.edu / 123
            };
        }

        public IList<Pessoa> ObterTodos() => _Pessoas;
        public Pessoa ObterPessoaPorEmail(string email)
        {
            var pessoa = _Pessoas.FirstOrDefault(p => p.Usuario?.Email?.Equals(email) ?? false);

            if (pessoa is Estudante aluno)
                return aluno;

            else if (pessoa is Professor professor)
                return professor;

            else if (pessoa is Admin admin)
                return admin;
            else
                return null;
        }
        public Pessoa ObterPessoaPorId(int id) => _Pessoas[id];
        public void AdcionarPessoa(TipoUsuario tipoUsuario, string nome, string sobrenome, string cpf, string tipoGraduacao, string curso)
        {
            if (tipoUsuario is TipoUsuario.Estudante)
                _Pessoas.Add(new Estudante(nome, sobrenome, cpf, tipoGraduacao, curso));

            else if (tipoUsuario is TipoUsuario.Professor)
            {
                int id = 100;
                foreach (var pessoa in _Pessoas)
                {
                    if(pessoa is Professor ||  pessoa is Admin admin)
                    {
                        id++;
                    }
                }
                _Pessoas.Add(new Professor(id, nome, sobrenome, cpf, tipoGraduacao, curso));
            }

            else if(tipoUsuario is TipoUsuario.Admin)
            {
                int id = 100;
                foreach (var pessoa in _Pessoas)
                {
                    if (pessoa is Professor || pessoa is Admin admin)
                    {
                        id++;
                    }
                }
                _Pessoas.Add(new Admin(nome, sobrenome, cpf, id));
            }
        } 
    }
}
