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
    
    static internal MedidorDTO ProcessarArquivoMedicao(string value)
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
    
    static internal List<MedidorDTO> ProcessarListaMedicao(string value)
    {
        string caminho = "";

        if (value.Equals("medicao"))
        {
            caminho = "resources/db/medicao.txt";
            if (!File.Exists(caminho))
            {
                return null;
            }
        
            return LeituraMedicao(caminho); 
        }
    
        return null;
    }
    
    static List<MedidorDTO> LeituraMedicao(string caminho)
    {
        List<MedidorDTO> listaMedicao = new List<MedidorDTO>();

        try
        {
            string[] lines = File.ReadAllLines(caminho);

            if (lines.Length == 0)
            {
                Console.WriteLine("O arquivo está vazio.");
                return listaMedicao;
            }

            MedidorDTO medidorDto = null;

            foreach (var line in lines)
            {
                // Ignorar linhas vazias ou delimitadores
                if (string.IsNullOrWhiteSpace(line) || line == ";")
                    continue;

                // Criar um novo objeto MedidorDTO quando encontrar uma nova leitura
                if (medidorDto == null)
                    medidorDto = new MedidorDTO();

                // Processar cada linha do arquivo
                if (PercorrerMedicao(line, medidorDto))
                {
                    // Adicionar o medidor completo à lista e criar um novo objeto
                    listaMedicao.Add(medidorDto);
                    medidorDto = null; 
                }
            }

            if (medidorDto != null)
                listaMedicao.Add(medidorDto);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao ler o arquivo: {ex.Message}");
        }

        return listaMedicao;
    }

    private static bool PercorrerMedicao(string line, MedidorDTO medidorDto)
    {
        bool medidorCompleto = false;

        try
        {
            if (line.Contains("DATA LEITURA:", StringComparison.CurrentCultureIgnoreCase))
            {
                if (DateTime.TryParse(line.Replace("DATA LEITURA:", "").Replace(";", "").Trim(), out DateTime dataLeitura))
                {
                    medidorDto.SetDataLeitura(dataLeitura);
                }
            }
            else if (line.Contains("ATIVO:", StringComparison.CurrentCultureIgnoreCase))
            {
                string consumo = line.Replace("ATIVO:", "").Replace("kWh", "").Replace(";", "").Trim();
                if (double.TryParse(consumo, out double ativo))
                {
                    medidorDto.SetConsumo(ativo);
                }
            }
            else if (line.Contains("NOME:", StringComparison.CurrentCultureIgnoreCase))
            {
                medidorDto.GetUsuario().SetNome(line.Replace("NOME:", "").Replace(";", "").Trim());
            }
            else if (line.Contains("CPF/CNPJ:", StringComparison.CurrentCultureIgnoreCase))
            {
                medidorDto.GetUsuario().SetIdentificador(line.Replace("CPF/CNPJ:", "").Replace(";", "").Trim());
            }
            else if (line.Contains("Apelido:") && line.Contains("Serial:"))
            {
                var dados = line.Split(',');
                foreach (var dado in dados)
                {
                    if (dado.Contains("Apelido:"))
                    {
                        medidorDto.SetApelido(dado.Replace("Apelido:", "").Trim());
                    }
                    else if (dado.Contains("Serial:"))
                    {
                        medidorDto.SetSerial(dado.Replace("Serial:", "").Trim());
                    }
                }

                medidorCompleto = true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao processar linha: {line}. Erro: {ex.Message}");
        }

        return medidorCompleto;
    }

    static internal MedidorDTO LeituraArquivoMedidor(string caminho)
    {
        Medidor medidorPrincipal = null;
        double ativo = 0;
        string apelido = "";
        string serial = "";

        try
        {
            var linhas = File.ReadAllLines(caminho);

            foreach (var linha in linhas)
            {
                if (linha.StartsWith("ATIVO:", StringComparison.OrdinalIgnoreCase))
                {
                    string valorAtivo = linha.Replace("ATIVO:", "").Replace("kWh", "").Replace(";", "").Trim();
                    if (double.TryParse(valorAtivo, out ativo))
                    {
                        ativo = double.Parse(valorAtivo);
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

                    if (apelido.Equals("medidor principal", StringComparison.OrdinalIgnoreCase))
                    {
                        medidorPrincipal = new Medidor(apelido, serial, true);
                        break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao ler o arquivo: {ex.Message}");
        }
        
        MedidorDTO medidorDto = new MedidorDTO(medidorPrincipal.GetApelido(), medidorPrincipal.GetSerial(), ativo);
        return medidorDto;
    }

}