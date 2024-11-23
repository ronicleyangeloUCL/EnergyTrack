using System;
using System.Collections.Generic;
using System.Linq;

public class Medidor
{
    private string _apelido;
    private string _serial;

    public bool IsPrincipal { get; set; }

    public Medidor() { }

    public Medidor(string apelido, string serial, bool isPrincipal = false)
    {
        this._apelido = apelido;
        this._serial = serial;
        this.IsPrincipal = isPrincipal;
    }

    public bool IsMedidor(string serial)
    {
        return this._serial.Equals(serial);
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

    public static List<Medidor> CadastroMedidor()
    {
        string? continuar = "S";
        List<Medidor> listaMedidores = new List<Medidor>();
        bool medidorPrincipalCadastrado = false;

        while (continuar.ToUpper() == "S")
        {
            if (!medidorPrincipalCadastrado)
            {
                Console.Clear();
                Console.WriteLine("Digite o serial do medidor principal (da concessionária):");
                string? serial = Console.ReadLine();

                bool isDuplicate = listaMedidores.Any(medidor => medidor.IsPrincipal);
                if (isDuplicate)
                {
                    Console.Clear();
                    Console.WriteLine("Já existe um medidor principal cadastrado. Não é possível cadastrar mais de um.");
                    continuar = "N"; 
                    break;
                }

                Medidor medidorPrincipal = new Medidor("MedidorPrincipal", serial, isPrincipal: true);
                listaMedidores.Add(medidorPrincipal);
                Console.Clear();
                Console.WriteLine("Medidor principal (Medidor da Concessionária) cadastrado com sucesso!");

                medidorPrincipalCadastrado = true; 
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Digite o apelido do medidor adicional:");
                string? apelido = Console.ReadLine();

                Console.WriteLine("Digite o serial do medidor adicional:");
                string? serial = Console.ReadLine();

                bool isDuplicate = listaMedidores.Any(medidor => medidor.GetSerial() == serial);
                if (isDuplicate)
                {
                    Console.Clear();
                    Console.WriteLine("Serial já existe na lista! Não foi possível cadastrar o medidor.");
                }
                else
                {
                    Medidor medidorAdicional = new Medidor(apelido, serial);
                    listaMedidores.Add(medidorAdicional);
                    Console.Clear();
                    Console.WriteLine("Medidor adicional cadastrado com sucesso!");
                }
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

    public override string ToString()
    {
        return $"Apelido: {this._apelido};, Serial: {this._serial};" + (IsPrincipal ? " (Principal)" : "");
    }

    public static List<Medidor> ListDTO(MedidorDTO dto)
    {
        return new List<Medidor> { new Medidor { _apelido = dto.GetApelido(), _serial = dto.GetSerial() } };
    }
}
