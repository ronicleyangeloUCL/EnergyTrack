using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Energytrack.core.domain;

public class Medicao
{
    private DateTime dataLeitura;
    private List<Medidor> medidor = new List<Medidor>();
    private double ativo;
    private Usuario usuario;

    public Medicao(){}
    public Medicao(DateTime dataLeitura, List<Medidor> medidor, double ativo, Usuario usuario)
    {
        this.dataLeitura = dataLeitura;
        this.medidor = medidor;
        this.ativo = ativo;
        this.usuario = usuario;
    }

    public DateTime GetDataLeitura() => this.dataLeitura;
    public List<Medidor> GetMedidor() => this.medidor;
    public double GetAtivo() => this.ativo;
    public Usuario GetUsuario() => this.usuario;

    public void SetMedidor(List<Medidor> value) => medidor = value;
    public void SetDataLeitura(DateTime value) => dataLeitura = value;
    public void SetAtivo(double value) => ativo = value;
    public void SetUsuario(Usuario value) => usuario = value;

    public void RegistraMedidor()
    {
        // string caminhoListUsuario = "resources/db/usuario.txt";
        string caminhoSaveMedicoes = "resources/db/medicao.txt";
        
        // Passar o nome da clase que deixar buscar os dados EX: usuario, medidor
        List<Usuario> listUsuario = Arquivo<Usuario>.ProcessarArquivo("usuario");
        List<Medicao> medicoes = new List<Medicao>();

        Console.Clear();
        Usuario usuarioAtual = Usuario.SolicitarUsuario(listUsuario);

        if (usuarioAtual != null)
        {
            Usuario.ExibirUsuario(usuarioAtual);
            RegistrarMedicoes(usuarioAtual, medicoes);

            Arquivo<Medicao> arquivo = new Arquivo<Medicao>(caminhoSaveMedicoes);
            arquivo.EscritaArquivo("", medicoes);
        }
        else
        {
            Console.WriteLine("Usuário não encontrado.");
        }
    }

    private void RegistrarMedicoes(Usuario usuario, List<Medicao> medicoes)
    {
        if (usuario is PessoaFisica pf)
        {
            foreach (var medidor in pf.GetMedidorList())
            {
                RegistrarMedicao(medidor, medicoes, usuario);
            }
        }
        else if (usuario is PessoaJuridica pj)
        {
            foreach (var medidor in pj.GetMedidorList())
            {
                RegistrarMedicao(medidor, medicoes, usuario);
            }
        }
    }
    private void RegistrarMedicao(Medidor medidor, List<Medicao> medicoes, Usuario usuario)
    {
        Console.WriteLine("----------------------------------------------------------------");
        Console.WriteLine($"Apelido: {medidor.GetApelido()}, Serial: {medidor.GetSerial()}");
        Console.WriteLine("----------------------------------------------------------------");

        DateTime dataLeitura = SolicitarDataLeitura();
        double consumo = SolicitarConsumo(medidor);

        Medicao medicao = new Medicao(dataLeitura, new List<Medidor> { medidor }, consumo, usuario);
        medicoes.Add(medicao);

        Console.WriteLine($"Medição registrada: {medicao}");
        Console.WriteLine();
    }

    private DateTime SolicitarDataLeitura()
    {
        Console.Write("Digite a data da leitura (dd/MM/yyyy): ");
        string dataInput = LerData();
        return DateTime.ParseExact(dataInput, "dd/MM/yyyy", null);
    }

    private double SolicitarConsumo(Medidor medidor)
    {
        double consumo = 0;
        bool isMedidorPrincipal = medidor.GetApelido() == "MedidorPrincipal";

        if (isMedidorPrincipal)
        {
            consumo = SolicitarConsumoMedidorPrincipal();
        }
        else
        {
            consumo = SolicitarConsumoGerais();
        }

        return consumo;
    }

    private double SolicitarConsumoMedidorPrincipal()
    {
        double consumo;
        while (true)
        {
            Console.Write("Digite o consumo ativo do medidor principal (kWh): ");
            string consumoInput = Console.ReadLine();

            if (double.TryParse(consumoInput, out consumo) && consumo > 0)
            {
                return consumo;
            }
            else
            {
                Console.WriteLine("Valor inválido. O consumo deve ser um número positivo.");
            }
        }
    }

    private double SolicitarConsumoGerais()
    {
        double consumo;
        while (true)
        {
            Console.Write("Digite o consumo ativo (kWh): ");
            string consumoInput = Console.ReadLine();

            if (double.TryParse(consumoInput, out consumo) && consumo > 0)
            {
                return consumo;
            }
            else
            {
                Console.WriteLine("Valor inválido. O consumo não pode ser nulo ou zerado.");
            }
        }
    }

    public static string LerData()
    {
        StringBuilder dataInput = new StringBuilder();

        while (dataInput.Length < 10)
        {
            var tecla = Console.ReadKey(intercept: true).KeyChar;

            if (Char.IsDigit(tecla))
            {
                dataInput.Append(tecla);

                if (dataInput.Length == 2 || dataInput.Length == 5)
                {
                    dataInput.Append("/");
                }

                ExibirData(dataInput.ToString());
            }
            else if (tecla == (char)8 && dataInput.Length > 0)
            {
                dataInput.Remove(dataInput.Length - 1, 1);
                if (dataInput.Length == 2 || dataInput.Length == 5) 
                {
                    dataInput.Remove(dataInput.Length - 1, 1);
                }

                ExibirData(dataInput.ToString());
            }
        }

        if (!ValidarData(dataInput.ToString()))
        {
            Console.WriteLine("\nData inválida. Tente novamente.");
            return LerData(); 
        }

        return dataInput.ToString();
    }

    private static void ExibirData(string data)
    {
        Console.Clear();
        Console.WriteLine($"Digite a data da leitura (dd/MM/yyyy): {data}");
    }

    private static bool ValidarData(string data)
    {
        DateTime dt;
        return DateTime.TryParseExact(data, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dt);
    }

    public override string ToString()
    {
        string medidoresInfo = string.Join("\n", medidor.Select(m => $"Apelido: {m.GetApelido()}, Serial: {m.GetSerial()}"));

        string identificador = ""; 

        if (usuario is PessoaFisica pf)
        {
            identificador = pf.GetCpf(); 
        }
        else if (usuario is PessoaJuridica pj)
        {
            identificador = pj.GetCnpj(); 
        }

        return $"DATA LEITURA: {dataLeitura.ToString("dd/MM/yyyy")};\n" +
               $"ATIVO: {ativo} kWh;\n" +
               $"NOME: {TransformarUpper(usuario.GetNome())};\n" +
               $"CPF/CNPJ: {identificador};\n" +  // Exibe o CPF ou CNPJ
               $"MEDIDOR:\n {medidoresInfo};\n ";
    }


    private string TransformarUpper(string value)
    {
        return value.ToUpper();
    }
}
