using System.Runtime.Serialization;

class Medidor
{
    private string _apelido;
    private string _serial;

    public Medidor() {}

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
    public string GetApelido => this._apelido;

    public void SetSerial(string serial)
    {
        this._serial = serial;
    }

    public void SetApelido(string apelido)
    {
        this._apelido = apelido;
    }
}