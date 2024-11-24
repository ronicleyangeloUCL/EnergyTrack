using System.Runtime.InteropServices;
using System.Text;
using Energytrack.core.domain;
public class Arquivo<T> : IArquivo<T>
{
    private string path;
    public Arquivo() { }
    public Arquivo(string path)
    {
        this.path = path;
    }

    public string GetPath() => this.path;

    public void SetPath(string path)
    {
        this.path = path;
    }

    public static List<Usuario> LeituraArquivo(string path)
    {
        string[] lines = File.ReadAllLines(path);

        PessoaJuridica pessoaJuridica = null;
        PessoaFisica pessoaFisica = null;

        List<Usuario> usuarios = InterarListaUsuarios(pessoaFisica, pessoaJuridica, lines);

        return usuarios;
    }

    static string ExtrairValor(string line, string chave)
    {
        return line.Split(':')[1].Trim(';', ' ');
    }

    static Medidor ProcessarLinhaMedidor(string line)
    {
        Medidor medidor = new Medidor();
        string[] parts = line.Split(';');

        foreach (string part in parts)
        {
            if (part.Contains("Apelido:"))
            {
                string apelido = part.Split(':')[1]?.Trim();
                if (!string.IsNullOrEmpty(apelido))
                {
                    medidor.SetApelido(apelido);
                }
                else
                {
                    Console.WriteLine("Apelido não encontrado ou vazio.");
                }
            }
            else if (part.Contains("Serial:"))
            {
                string serial = part.Split(':')[1]?.Trim();
                if (!string.IsNullOrEmpty(serial))
                {
                    medidor.SetSerial(serial);
                }
                else
                {
                    Console.WriteLine("Serial não encontrado ou vazio.");
                }
            }
        }
        return medidor;
    }

    public void EscritaArquivo<T>(string cabecalho, List<T> t)
    {
        try
        {
            FileStream fileStream = new FileStream(path, FileMode.Append, FileAccess.Write);
            StreamWriter fluxoEscrita = new StreamWriter(fileStream, Encoding.UTF8);

            foreach (T dado in t)
            {
                fluxoEscrita.WriteLine($"{dado};");
            }
            fluxoEscrita.WriteLine();
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

    private static List<Usuario> InterarListaUsuarios(PessoaFisica pessoaFisica, PessoaJuridica pessoaJuridica, string[] lines)
    {
        List<Usuario> usuarioList = new List<Usuario>();

        foreach (string line in lines)
        {
            if (line.StartsWith("Nome da Empresa:"))
            {
                // Se uma pessoa jurídica foi processada, adicione ela à lista
                if (pessoaJuridica != null)
                {
                    usuarioList.Add(pessoaJuridica);
                }

                pessoaJuridica = new PessoaJuridica();
                pessoaJuridica.SetNome(ExtrairValor(line, "Nome da Empresa:"));
            }
            else if (line.StartsWith("CNPJ:"))
            {
                pessoaJuridica.SetCnpj(ExtrairValor(line, "CNPJ:"));
            }
            else if (line.StartsWith("Nome:"))
            {
                // Se houver uma pessoa física anterior, adicione ela à lista
                if (pessoaFisica != null)
                {
                    usuarioList.Add(pessoaFisica);
                }

                pessoaFisica = new PessoaFisica();
                pessoaFisica.SetNome(ExtrairValor(line, "Nome:"));
            }
            else if (line.StartsWith("CPF:"))
            {
                if (pessoaFisica != null)
                {
                    pessoaFisica.SetCpf(ExtrairValor(line, "CPF:"));
                }
            }
            else if (line.Trim().StartsWith("0.") || line.Trim().StartsWith("1.") || line.Trim().StartsWith("2."))
            {
                Medidor medidor = ProcessarLinhaMedidor(line);

                if (pessoaJuridica != null)
                {
                    pessoaJuridica.GetMedidorList().Add(medidor);
                }

                if (pessoaFisica != null)
                {
                    pessoaFisica.GetMedidorList().Add(medidor);
                }
            }
        }

        if (pessoaJuridica != null)
        {
            usuarioList.Add(pessoaJuridica);
        }
        if (pessoaFisica != null)
        {
            usuarioList.Add(pessoaFisica);
        }

        return usuarioList;
    }
    public bool ArquivoExiste()
    {
        return File.Exists(path);
    }

    static internal List<Usuario> ProcessarArquivo(string value)
    {
        string caminho = "";

        if (value.Equals("usuario"))
        {
            List<Usuario> usuarios = new List<Usuario>();

            caminho = "resources/db/usuario.txt";
            if (!File.Exists(caminho))
            {
                return null;
            }
            return usuarios = LeituraArquivo(caminho);
        }
        return null;
    }
    
    static internal Medidor ProcessarArquivoMedicao(string value)
    {
        string caminho = "";

        if (value.Equals("medicao"))
        {
            caminho = "resources/db/medicao.txt";
            if (!File.Exists(caminho))
            {
                return null;
            }
        
            return LeituraArquivoMedidor(caminho); 
        }
    
        return null;
    }
    
    static internal Medidor LeituraArquivoMedidor(string caminho)
    {
        Medidor medidorPrincipal = null;
        double ativo = 0;
        string apelido = "";
        string serial = "";

        try
        {
            // Lê todas as linhas do arquivo
            var linhas = File.ReadAllLines(caminho);

            foreach (var linha in linhas)
            {
                Console.WriteLine($"Processando linha: {linha}");

                // Processa a linha que contém "ATIVO:"
                if (linha.StartsWith("ATIVO:", StringComparison.OrdinalIgnoreCase))
                {
                    string valorAtivo = linha.Replace("ATIVO:", "").Replace("kWh", "").Replace(";", "").Trim();
                    if (double.TryParse(valorAtivo, out ativo))
                    {
                        Console.WriteLine($"Ativo encontrado: {ativo} kWh");
                    }
                    else
                    {
                        Console.WriteLine($"Erro ao processar o valor do ativo na linha: {linha}");
                    }
                }
                else if (linha.Contains("Apelido:") && linha.Contains("Serial:"))
                {
                    var dados = linha.Split(',');
                    foreach (var dado in dados)
                    {
                        if (dado.Contains("Apelido:"))
                        {
                            apelido = dado.Replace("Apelido:", "").Trim();
                        }
                        else if (dado.Contains("Serial:"))
                        {
                            serial = dado.Replace("Serial:", "").Replace(";", "").Trim();
                        }
                    }

                    Console.WriteLine($"Apelido encontrado: {apelido}");
                    Console.WriteLine($"Serial encontrado: {serial}");

                    if (apelido.Equals("medidor principal", StringComparison.OrdinalIgnoreCase))
                    {
                        medidorPrincipal = new Medidor(apelido, serial);
                        Console.WriteLine($"Medidor Principal Encontrado: Apelido = {apelido}, Serial = {serial}, Ativo = {ativo} kWh");
                        break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao ler o arquivo: {ex.Message}");
        }

        if (medidorPrincipal == null)
        {
            Console.WriteLine("Medidor Principal não encontrado.");
        }
        else
        {
            Console.WriteLine($"Medidor Principal: {medidorPrincipal.GetApelido()} | {medidorPrincipal.GetSerial()} | Ativo: {ativo} kWh");
        }

        return medidorPrincipal;
    }

}