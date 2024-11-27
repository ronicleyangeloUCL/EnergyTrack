using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Energytrack.core.domain;

public class Medicao
{
    private DateTime DataLeitura { get; set; }
    private List<Medidor> Medidores { get; set; } = new List<Medidor>();
    private double Ativo { get; set; }
    private Usuario Usuario { get; set; }

    public Medicao() { }

    public Medicao(DateTime dataLeitura, List<Medidor> medidores, double ativo, Usuario usuario)
    {
        DataLeitura = dataLeitura;
        Medidores = medidores;
        Ativo = ativo;
        Usuario = usuario;
    }

    // Métodos Getters e Setters
    public DateTime GetDataLeitura() => DataLeitura;
    public List<Medidor> GetMedidores() => Medidores;
    public double GetAtivo() => Ativo;
    public Usuario GetUsuario() => Usuario;

    public void SetMedidores(List<Medidor> value) => Medidores = value;
    public void SetDataLeitura(DateTime value) => DataLeitura = value;
    public void SetAtivo(double value) => Ativo = value;
    public void SetUsuario(Usuario value) => Usuario = value;

    // Registro de medição
    public void RegistraMedidor()
    {
        string caminhoSaveMedicoes = "resources/db/medicao.txt";

        List<Usuario> listUsuario = Arquivo<Usuario>.ProcessarArquivo("usuario".ToLower());
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
        while (true)
        {
            Console.Write("Digite a data da leitura (dd/MM/yyyy): ");
            string dataInput = Console.ReadLine();

            if (DateTime.TryParseExact(dataInput, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime data))
            {
                return data;
            }
            else
            {
                Console.WriteLine("Data inválida. Tente novamente.");
            }
        }
    }

    private double SolicitarConsumo(Medidor medidor)
    {
        while (true)
        {
            Console.Write("Digite o consumo ativo (kWh): ");
            string consumoInput = Console.ReadLine();

            if (double.TryParse(consumoInput, out double consumo) && consumo > 0)
            {
                return consumo;
            }
            else
            {
                Console.WriteLine("Valor inválido. O consumo deve ser um número positivo.");
            }
        }
    }

    // Visualizar consumo
    public void VisualizarConsumo()
    {
        List<Usuario> listUsuario = Arquivo<Usuario>.ProcessarArquivo("usuario".ToLower());

        Console.Clear();
        Usuario usuarioAtual = Usuario.SolicitarUsuario(listUsuario);

        if (usuarioAtual != null)
        {
            Usuario.ExibirUsuario(usuarioAtual);

            List<MedidorDTO> medicao = Arquivo<MedidorDTO>.ProcessarListaMedicao("medicao");

            if (medicao != null && medicao.Count > 0)
            {
                Console.WriteLine("\n--- Consumo dos Medidores ---");
                Console.WriteLine("Data Leitura   | Ativo (kWh) | Apelido            | Serial");
                Console.WriteLine("------------------------------------------------------------");

                medicao.ForEach(med =>
                {
                    Console.WriteLine($"{med.GetDataLeitura():dd/MM/yyyy}   | {med.GetConsumo()} kwh | {med.GetApelido(),-18} | {med.GetSerial(),10:F2}");
                });
            }
            else
            {
                Console.WriteLine("\nNenhum medidor encontrado.");
            }
        }
        else
        {
            Console.WriteLine("Usuário não encontrado.");
        }
    }

    // Método ToString
    public override string ToString()
    {
        string medidoresInfo = string.Join("\n", Medidores.Select(m => $"Apelido: {m.GetApelido()}, Serial: {m.GetSerial()}"));

        return $"DATA LEITURA: {DataLeitura:dd/MM/yyyy};\n" +
               $"ATIVO: {Ativo} kWh;\n" +
               $"NOME: {Usuario.GetNome().ToUpper()};\n" +
               $"CPF/CNPJ: {Usuario.GetIdentificador()};\n" +
               $"MEDIDOR:\n {medidoresInfo};\n ";
    }
}
