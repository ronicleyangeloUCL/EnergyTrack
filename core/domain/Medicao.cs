using System;
using System.Collections.Generic;
using System.Linq;
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
        string caminhoListUsuario = "resources/db/usuario.txt";
        string caminhoSaveMedicoes = "resources/db/medicao.txt";

        List<Usuario> listUsuario = Arquivo<Usuario>.LeituraArquivo(caminhoListUsuario);
        List<Medicao> medicoes = new List<Medicao>();

        Console.Clear();
        Usuario usuarioAtual = SolicitarUsuario(listUsuario);

        if (usuarioAtual != null)
        {
            ExibirUsuario(usuarioAtual);
            RegistrarMedicoes(usuarioAtual, medicoes);

            Arquivo<Medicao> arquivo = new Arquivo<Medicao>(caminhoSaveMedicoes);
            arquivo.EscritaArquivo("", medicoes);
        }
        else
        {
            Console.WriteLine("Usuário não encontrado.");
        }
    }

    private Usuario SolicitarUsuario(List<Usuario> listUsuario)
    {
        Console.WriteLine("Digite o CPF (Pessoa Física) ou CNPJ (Pessoa Jurídica) do usuário para registrar medições:");
        string identificador = Console.ReadLine().Trim();

        foreach (var usuario in listUsuario)
        {
            if ((usuario is PessoaFisica pf && pf.GetCpf() == identificador) ||
                (usuario is PessoaJuridica pj && pj.GetCnpj() == identificador))
            {
                return usuario;
            }
        }

        return null;
    }


    private void ExibirUsuario(Usuario usuario)
    {
        if (usuario is PessoaFisica pf)
        {
            Console.WriteLine($"Pessoa Física: {pf.GetNome().ToUpper()}, CPF: {pf.GetCpf()}");
        }
        else if (usuario is PessoaJuridica pj)
        {
            Console.WriteLine($"Pessoa Jurídica: {pj.GetNome().ToUpper()}, CNPJ: {pj.GetCnpj()}");
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
        string dataInput = ExibirFormatoData();
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

    private string ExibirFormatoData()
    {
        string dataInput = "";

        while (dataInput.Length < 10)
        {
            var tecla = Console.ReadKey(intercept: true).KeyChar;

            if (Char.IsDigit(tecla))
            {
                dataInput += tecla;

                if (dataInput.Length == 2 || dataInput.Length == 5)
                {
                    dataInput += "/";
                }

                Console.Clear();
                Console.WriteLine($"Digite a data da leitura (dd/MM/yyyy): {dataInput}");
            }
            else if (tecla == (char)8 && dataInput.Length > 0)
            {
                dataInput = dataInput.Substring(0, dataInput.Length - 1);
                Console.Clear();
                Console.WriteLine($"Digite a data da leitura (dd/MM/yyyy): {dataInput}");
            }
        }

        return dataInput;
    }

    public override string ToString()
    {
        string medidoresInfo = string.Join("\n", medidor.Select(m => $"Apelido: {m.GetApelido()}, Serial: {m.GetSerial()}"));

        return $"DATA LEITURA: {dataLeitura.ToString("dd/MM/yyyy")};\n" +
               $"ATIVO: {ativo} kWh;\n" +
               $"USUÁRIO: {TransformarUpper(usuario.GetNome())};\n" +
               $"MEDIDOR:\n {medidoresInfo};";
    }

    private string TransformarUpper(string value)
    {
        return value.ToUpper();
    }
}
