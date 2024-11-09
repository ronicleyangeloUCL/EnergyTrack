public interface IArquivo<T>
{
    public void EscritaArquivo(string cabecalho, List<T> t){}

    public bool leituraArquivo(List<T> t) => false;
}