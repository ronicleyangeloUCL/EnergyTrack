public interface IArquivo<T>
{
    public void EscritaArquivo(string cabecalho, List<T> t){}

    public List<T> LeituraArquivo(List<T> t) { return t;}

    public List<T> ProcessarArquivo(string value)
    {
        List<T> t = new List<T>();
        return t;
    }
}