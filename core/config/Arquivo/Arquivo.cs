using System.Text;

class Arquivo
{
    private string path;

    public Arquivo(string path)
    {
        this.path = path;
    }

    public string GetPath() => this.path;

    public void SetPath(string path)
    {
        this.path = path;
    }

    public void LeituraArquivo()
    {
        Console.WriteLine("Tentando abrir o arquivo: " + path);
        try
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            {
                while (!sr.EndOfStream)
                {
                    string str = sr.ReadLine();
                    Console.WriteLine(str);
                }
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Arquivo não encontrado: " + path);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro: " + ex.Message);
        }
    }

    public void EscritaArquivo(string cabecalho, List<string> dados)
    {
        try
        {
            FileStream fileStream = new FileStream(path, FileMode.Append, FileAccess.Write);
            StreamWriter fluxoEscrita = new StreamWriter(fileStream, Encoding.UTF8);

            // Escreve o cabeçalho no arquivo
            fluxoEscrita.WriteLine("--------------------------");
            fluxoEscrita.WriteLine(cabecalho);
            fluxoEscrita.WriteLine("--------------------------");

            // Escreve os dados do abastecimento no arquivo
            foreach (string dado in dados)
            {
                fluxoEscrita.WriteLine($"{dado};");
            }
            fluxoEscrita.WriteLine();
            // Fecha o arquivo
            fluxoEscrita.Close();
            fileStream.Close();
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Arquivo não encontrado: " + path);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro: " + ex.Message);
        }
    }

    public bool ArquivoExiste()
    {
        return File.Exists(path);
    }

    public void ModeloArquivo()
    {

    }
}