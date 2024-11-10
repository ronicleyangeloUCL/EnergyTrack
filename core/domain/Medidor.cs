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
    public override string ToString()
    {
        return $"Apelido: {this._apelido}, Serial: {this._serial}";
    }
}