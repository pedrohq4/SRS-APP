using SRS.Faculdade.APP.Model.Academico;
using SRS.Faculdade.APP.Model.Entities;
using System.Collections.Generic;
using System.Linq;

public class HistoricoTurma
{
    public string Nota1 { get; set; }
    public string Nota2 { get; set; }
    public string Nota3 { get; set; }
    public string Nota4 { get; set; }
    public Estudante Estudante { get; set; }
    public Turma Turma { get; set; }

    private static readonly Dictionary<string, int> ConceitoParaValor = new Dictionary<string, int>
    {
        { "ND", 0 },
        { "D", 1 },
        { "DM", 2 },
        { "DML", 3 }
    };

    private static readonly Dictionary<int, string> ValorParaConceito = new Dictionary<int, string>
    {
        { 0, "ND" },
        { 1, "D" },
        { 2, "DM" },
        { 3, "DML" }
    };

    public HistoricoTurma(Estudante estudante, Turma turma)
    {
        Turma = turma;
        Estudante = estudante;
    }

    public static bool EhNotaValida(string nota)
    {
        return ConceitoParaValor.ContainsKey(nota?.ToUpper() ?? string.Empty);
    }

    public static bool FoiAprovado(string nota)
    {
        if (!EhNotaValida(nota))
            return false;

        return !nota.ToUpper().Equals("ND");
    }

    public string CalcularMediaFinal()
    {
        List<int> valoresNotas = new List<int>();

        if (EhNotaValida(Nota1)) valoresNotas.Add(ObterValorDoConceito(Nota1));
        if (EhNotaValida(Nota2)) valoresNotas.Add(ObterValorDoConceito(Nota2));
        if (EhNotaValida(Nota3)) valoresNotas.Add(ObterValorDoConceito(Nota3));
        if (EhNotaValida(Nota4)) valoresNotas.Add(ObterValorDoConceito(Nota4));

        if (valoresNotas.Count == 0)
            return "ND";

        double mediaNumerica = valoresNotas.Average();
        int valorMedioArredondado = (int)System.Math.Round(mediaNumerica);
        valorMedioArredondado = System.Math.Clamp(valorMedioArredondado, 0, 3);

        return ValorParaConceito[valorMedioArredondado];
    }

    public bool EstaAprovado()
    {
        string mediaFinal = CalcularMediaFinal();
        return FoiAprovado(mediaFinal);
    }

    public static int ObterValorDoConceito(string conceito)
    {
        if (conceito == null || !ConceitoParaValor.TryGetValue(conceito.ToUpper(), out int valor))
        {
            return 0; // Retorna o valor padrão (ND) se o conceito for inválido
        }
        return valor;
    }

    public static string ObterConceitoDoValor(int valor)
    {
        return ValorParaConceito.TryGetValue(valor, out string conceito) ? conceito : "ND";
    }
}