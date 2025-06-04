using SRS.Faculdade.APP.Model.Pessoa;

namespace SRS.Faculdade.APP.Services
{
    public class PessoaService : IPessoaService
    {
        private IList<Usuario> _usuarios;

        public PessoaService() => CarregarListaInicial();

        public void CarregarListaInicial()
        {
            _usuarios = new List<Usuario>()
            {
                new Usuario(false, TipoUsuario.Professor, "Jose", "PereiraSilva", "0987392", "Professor", "Humanas"),
                new Usuario(false, TipoUsuario.Professor, "João", "Silva", "0987392", "Professor", "Humanas"),
                new Usuario(false, TipoUsuario.Aluno, "A", "a", "0", "Doutorado", "Medicina")
            };
        }

        public IList<Usuario> ObterTodos() => _usuarios;
    }
}
