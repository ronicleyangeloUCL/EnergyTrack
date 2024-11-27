using Energytrack.core.domain;

public class MedidorDTO 
{
    private string _serial = "";
    private string _apelido = "";
    private double _consumo;
    private DateTime _dataLeitura;
    private Usuario _usuario;
    public MedidorDTO() {}

    public MedidorDTO(string serial, string apelido, double consumo) {
        this._serial = serial;
        this._apelido = serial;
        this._consumo = consumo;
    }

    public MedidorDTO(string serial, string apelido, double consumo, DateTime dataLeitura, Usuario usuario)
    {
        this._serial = serial;
        this._apelido = apelido;
        this._consumo = consumo;
        this._dataLeitura = dataLeitura;
        this._usuario = usuario;
    }

    public string GetApelido() => this._apelido;
    public string GetSerial() => this._serial;
    public double GetConsumo() => this._consumo;
    public DateTime GetDataLeitura() => this._dataLeitura;
    public Usuario GetUsuario() => this._usuario;
    
    public void SetApelido(string value) => _apelido = value;
    public void SetSerial(string value) => _serial = value;
    
    public void SetConsumo(double value) => _consumo = value;
    
    public void SetDataLeitura(DateTime value) => _dataLeitura = value;
    public void SetUsuario(Usuario value) => _usuario = value;

    public override string ToString() 
    {
        return $"Serial: {_serial}, Apelido: {_apelido}, Consumo: {_consumo:F2}, " +
               $"Data de Leitura: {_dataLeitura:yyyy-MM-dd}, Usuário: {_usuario?.ToString() ?? "N/A"}";
    }
}