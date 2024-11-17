using System.ComponentModel;
using System.Runtime.Serialization;

public class Medidor
{
    private string _apelido;
    private string _serial;

    public Medidor() { }

    public Medidor(string apelido, string serial)
    {
        this._apelido = apelido;
        this._serial = serial;
    }

    public Medidor(MedidorDTO medidorDTO)
    {
        this._apelido = medidorDTO.GetApelido();
        this._serial = medidorDTO.GetSerial();
    }

    public bool IsMedidor(string serial)
    {
        return this._serial.Equals(serial) ? true : false;
    }

    public string GetSerial() => this._serial;
    public string GetApelido() => this._apelido;

    public void SetSerial(string serial)
    {
        this._serial = serial;
    }

    public void SetApelido(string apelido)
    {
        this._apelido = apelido;
    }

    public static List<Medidor> MedidoresList(string apelido, string serial)
    {
        List<Medidor> list = new List<Medidor>();
        Medidor medidor = new Medidor(apelido, serial);
        list.Add(medidor);
        return list;
    }

    public static bool isSerial(List<Medidor> list)
    {

        return true;
    }


    public static List<Medidor> CadastroMedidor()
    {
        string? continuar = "S";

        List<Medidor> listaMedidores = new List<Medidor>();

        while (continuar.ToUpper() == "S")
        {
            Console.Clear();
            Console.WriteLine("Digite o apelido do medidor:");
            string? apelido = Console.ReadLine();

            Console.WriteLine("Digite o serial do medidor:");
            string? serial = Console.ReadLine();

            // Verificação de duplicidade
            bool isDuplicate = false;
            foreach (var medidor in listaMedidores)
            {
                if (serial.Equals(medidor.GetSerial(), StringComparison.OrdinalIgnoreCase))
                {
                    Console.Clear();
                    Console.WriteLine("Serial já existe na lista! Não foi possível cadastrar o medidor.");
                    isDuplicate = true;
                    break;
                }
            }

            // Caso não seja duplicado, cadastra o medidor
            if (!isDuplicate)
            {
                Medidor medidor = new Medidor(apelido, serial);
                listaMedidores.Add(medidor);
                Console.WriteLine();
                Console.WriteLine("Medidor cadastrado com sucesso!");
            }
            Console.WriteLine();
            Console.WriteLine("Deseja cadastrar outro medidor? (S/N)");
            continuar = Console.ReadLine()?.ToUpper();

            while (continuar != "S" && continuar != "N")
            {
                Console.WriteLine("Erro: Entrada inválida. Digite S para sim ou N para não.");
                continuar = Console.ReadLine()?.ToUpper();
            }
        }
        return listaMedidores;
    }

    public static List<Medidor> ListDTO(MedidorDTO dto)
    {
        return new List<Medidor> { new Medidor { _apelido = dto.GetApelido(), _serial = dto.GetSerial() } };
    }
    public override string ToString()
    {
        return $"Apelido: {this._apelido}, Serial: {this._serial}";
    }
}