using SRS.Faculdade.APP.View;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Faculdade.APP.Model.Entities
{
    public class Usuario
    {
        public TipoUsuario TipoUsuario { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool EhAdmin { get; set; }

        public Usuario(TipoUsuario tipoUsuario, string nome, string sobrenome)
        {
            Email = GerarEmail(nome, sobrenome, tipoUsuario);
            EhAdmin = false;
            Senha = "123";
            TipoUsuario = tipoUsuario;

            if(tipoUsuario is TipoUsuario.Admin)
            {
               EhAdmin = true;
            }
        }

        public string GerarEmail(string nome, string sobrenome, TipoUsuario tipoUsuario)
        {
            string RemoverAcentos(string texto)
            {
                var normalized = texto.Normalize(NormalizationForm.FormD);
                var sb = new StringBuilder();

                foreach (var c in normalized)
                {
                    var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                    if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                        sb.Append(c);
                }

                return sb.ToString().Normalize(NormalizationForm.FormC);
            }

            nome = RemoverAcentos(nome);
            sobrenome = RemoverAcentos(sobrenome);

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

            if (tipoUsuario is TipoUsuario.Estudante)
                return $"{nome.ToLower()}.{sobrenomeFormatado.ToLower()}@Aluno.edu";
            else if (tipoUsuario is TipoUsuario.Professor)
                return $"{nome.ToLower()}.{sobrenomeFormatado.ToLower()}@Professor.edu";
            else if(tipoUsuario is TipoUsuario.Admin)
                return $"{nome.ToLower()}.{sobrenomeFormatado.ToLower()}@Admin.edu";

            return string.Empty;
        }
    }
}
