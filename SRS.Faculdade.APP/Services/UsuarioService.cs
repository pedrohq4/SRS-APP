using SRS.Faculdade.APP.Model.Academico;
using SRS.Faculdade.APP.Model.Entities;

namespace SRS.Faculdade.APP.Services
{
    public class UsuarioService
    {
        private IList<Pessoa> _Pessoas;

        public UsuarioService() => CarregarListaInicial();

        public void CarregarListaInicial()
        {
            if(_Pessoas == null)
            _Pessoas = new List<Pessoa>()
            {
                //new Usuario(TipoUsuario.Professor, "Jose", "Pereira", "0", "Professor", "Humanas"), //jose.pereira@Professor.edu / 0987392
                /*new Professor("João", "Silva", "0", "Professor", "Humanas"),*/ //joão.silva@Professor.edu / 0987392
                new Estudante("Lucas", "Andrade", "0", "Doutorado", "Medicina") //lucas.andrade@Aluno.edu / 123
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
            else
                return pessoa;
        }
        public Pessoa ObterPessoaPorId(int id) => _Pessoas[id];


    }
}
