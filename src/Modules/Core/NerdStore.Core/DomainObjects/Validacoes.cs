using System.Text.RegularExpressions;

namespace NerdStore.Modules.Core.DomainObjects;

// AssertionConcern
public class Validacoes 
{   
    #region ValidarSeIgual
    public static void ValidarSeIgual(object object1, object object2, string mensagem)
    {
        if (object1.Equals(object2)) throw new DomainException(mensagem);
    }
    #endregion    

    #region ValidarSeDiferente
    public static void ValidarSeDiferente(object object1, object object2, string mensagem)
    {
        if (!object1.Equals(object2)) throw new DomainException(mensagem);
    }

    public static void ValidarSeDiferente(string pattern, string valor, string mensagem)
    {
        var regex = new Regex(pattern);

        if (!regex.IsMatch(valor)) throw new DomainException(mensagem);
    }
    #endregion

    #region ValidarTamanho
    public static void ValidarTamanho(string valor, int maximo, string mensagem)
    {
        var length = valor.Trim().Length;
        if (length > maximo) throw new DomainException(mensagem);
    }

    public static void ValidarTamanho(string valor, int minimo, int maximo, string mensagem)
    {
        var length = valor.Trim().Length;
        if (length < minimo || length > maximo) throw new DomainException(mensagem);
    }
    #endregion

    #region ValidarSeVazio
    public static void ValidarSeVazio(string valor, string mensagem)
    {
        if (valor == null || valor.Trim().Length == 0) throw new DomainException(mensagem);
    }
    #endregion

    #region ValidarSeNulo
    public static void ValidarSeNulo(object object1, string mensagem)
    {
        if (object1 == null) throw new DomainException(mensagem);
    }
    #endregion

    #region ValidarMinimoMaximo
    public static void ValidarMinimoMaximo(double valor, double minimo, double maximo, string mensagem)
    {
        if (valor < minimo || valor > maximo) throw new DomainException(mensagem);        
    }

    public static void ValidarMinimoMaximo(float valor, float minimo, float maximo, string mensagem)
    {
        if (valor < minimo || valor > maximo) throw new DomainException(mensagem);        
    }

    public static void ValidarMinimoMaximo(int valor, int minimo, int maximo, string mensagem)
    {
        if (valor < minimo || valor > maximo) throw new DomainException(mensagem);        
    }

    public static void ValidarMinimoMaximo(long valor, long minimo, long maximo, string mensagem)
    {
        if (valor < minimo || valor > maximo) throw new DomainException(mensagem);        
    }

    public static void ValidarMinimoMaximo(decimal valor, decimal minimo, decimal maximo, string mensagem)
    {
        if (valor < minimo || valor > maximo) throw new DomainException(mensagem);        
    }
    #endregion

    #region ValidarSeMenorQue
    public static void ValidarSeMenorQue(long valor, long minimo, string mensagem)
    {
        if (valor < minimo) throw new DomainException(mensagem);        
    }
    #endregion

    #region ValidarSeMenorQue
    public static void ValidarSeMenorQue(double valor, double minimo, string mensagem)
    {
        if (valor < minimo) throw new DomainException(mensagem);    
    }
    #endregion

    #region ValidarSeMenorQue
    public static void ValidarSeMenorQue(decimal valor, decimal minimo, string mensagem)
    {
        if (valor < minimo) throw new DomainException(mensagem);
    }
    #endregion

    #region ValidarSeMenorQue
    public static void ValidarSeMenorQue(int valor, int minimo, string mensagem)
    {
        if (valor < minimo) throw new DomainException(mensagem);        
    }
    #endregion

    #region ValidarSeFalso
    public static void ValidarSeFalso(bool boolValor, string mensagem)
    {
        if (!boolValor) throw new DomainException(mensagem);
    }
    #endregion

    #region ValidarSeVerdadeiro
    public static void ValidarSeVerdadeiro(bool boolValor, string mensagem)
    {
        if (boolValor) throw new DomainException(mensagem);
    }
    #endregion
}
