using System.Security.Cryptography;
using Energytrack.core.domain;
public class Medicao
{
    private DateTime dataLeitura;
    private List<Medidor> medidor = new List<Medidor>();
    private double ativo;

    public Medicao() { }
    public Medicao(DateTime dataLeitura, List<Medidor> medidor, double ativo)
    {
        this.dataLeitura = dataLeitura;
        this.medidor = medidor;
        this.ativo = ativo;
    }

    public DateTime GetDataLeitura() => this.dataLeitura;

    public List<Medidor> GetMedidor() => this.medidor;

    public double GetAtivo() => this.ativo;

    public void SetMedidor(List<Medidor> value) => medidor = value;

    public void SetDataLeitura(DateTime value) => dataLeitura = value;

    public void SetAtivo(double value) => ativo = value;

    public void RegistroMedidor()
    {
        string caminhoListUsuario = "resources/usuario.txt";
        string caminhoSaveMedicoes = "resources/db/medicao.txt";

        // Leitura de usuários do arquivo
        List<Usuario> listUsuario = Arquivo<Usuario>.LeituraArquivo(caminhoListUsuario);
        List<Medicao> medicoes = new List<Medicao>();

        // Solicita o CPF ou CNPJ do usuário atual
        Console.WriteLine("Digite o CPF (Pessoa Física) ou CNPJ (Pessoa Jurídica) do usuário para registrar medições:");
        string identificador = Console.ReadLine().Trim();  // CPF ou CNPJ

        Usuario usuarioAtual = null;

        // Encontra o usuário correspondente
        foreach (var usuario in listUsuario)
        {
            Console.WriteLine("dados do usuario",usuario is PessoaFisica pfs && pfs.GetCpf() == identificador);
            if (usuario is PessoaFisica pf && pf.GetCpf() == identificador)
            {
                usuarioAtual = pf;
                break;
            }
            else if (usuario is PessoaJuridica pj && pj.GetCnpj() == identificador)
            {
                usuarioAtual = pj;
                break;
            }
        }

        if (usuarioAtual != null)
        {
            if (usuarioAtual is PessoaFisica pf)
            {
                Console.WriteLine($"Pessoa Física: {pf.GetNome()}, CPF: {pf.GetCpf()}");
                // Processar medições para os medidores da Pessoa Física (se houver)
                foreach (var medidor in pf.GetMedidorList())
                {
                    RegistrarMedicao(medidor, medicoes);
                }
            }
            else if (usuarioAtual is PessoaJuridica pj)
            {
                Console.WriteLine("------------------------------------");
                Console.WriteLine($"Pessoa Jurídica: {pj.GetNome()}, CNPJ: {pj.GetCnpj()}");
                Console.WriteLine("------------------------------------");
                Console.WriteLine("Medidores:");

                // Processar medições para os medidores da Pessoa Jurídica (se houver)
                foreach (var medidor in pj.GetMedidorList())
                {
                    RegistrarMedicao(medidor, medicoes);
                }
            }

            Arquivo<Medicao> arquivo = new Arquivo<Medicao>(caminhoSaveMedicoes);
            arquivo.EscritaArquivo("", medicoes);
        }
        else
        {
            Console.WriteLine("Usuário não encontrado.");
        }
    }

    private void RegistrarMedicao(Medidor medidor, List<Medicao> medicoes)
    {
        Console.WriteLine($"  Apelido: {medidor.GetApelido()}, Serial: {medidor.GetSerial()}");
        Console.Write("Digite a data da leitura (dd/MM/yyyy): ");
        string dataInput = Console.ReadLine();
        DateTime dataLeitura;

        while (!DateTime.TryParseExact(dataInput, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dataLeitura))
        {
            Console.Write("Formato inválido. Digite novamente a data da leitura (dd/MM/yyyy): ");
            dataInput = Console.ReadLine();
        }

        Console.Write("Digite o consumo ativo (kWh): ");
        string ativoInput = Console.ReadLine();
        double ativo;

        while (!double.TryParse(ativoInput, out ativo))
        {
            Console.WriteLine();
            Console.Write("Valor inválido. Digite novamente o consumo ativo (kWh): ");
            ativoInput = Console.ReadLine();
        }

        Medicao medicao = new Medicao(dataLeitura, new List<Medidor> { medidor }, ativo);
        medicoes.Add(medicao);
        Console.WriteLine($"Medição registrada: {medicao}");
    }

    public override string ToString()
    {
        string medidoresInfo = string.Join(", ", medidor.Select(m => m.ToString()));

        return $"{medidoresInfo} | Data: {dataLeitura.ToString("dd/MM/yyyy")} | Consumo: {ativo} kWh";
    }


}