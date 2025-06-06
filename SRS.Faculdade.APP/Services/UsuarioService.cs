using SRS.Faculdade.APP.Model.Pessoa;

namespace SRS.Faculdade.APP.Services
{
    public class UsuarioService : IUsuarioService
    {
        private IList<Usuario> _usuarios;

        public UsuarioService() => CarregarListaInicial();

        public void CarregarListaInicial()
        {
            _usuarios = new List<Usuario>()
            {
                new Usuario(TipoUsuario.Professor, "Jose", "Pereira", "0987392", "Professor", "Humanas"), //jose.pereira@Professor.edu / 0987392
                new Usuario(TipoUsuario.Professor, "João", "Silva", "0987392", "Professor", "Humanas"), //joão.silva@Professor.edu / 0987392
                new Usuario(TipoUsuario.Aluno, "Lucas", "Andrade", "023334", "Doutorado", "Medicina") //Lucas.Andrade@Professor.edu / 023334
            };
        }

        public IList<Usuario> ObterTodos() => _usuarios;
    }
}
