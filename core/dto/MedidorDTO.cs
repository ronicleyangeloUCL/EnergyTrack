using Energytrack.core.domain;

public class MedidorDTO 
{
    private string _serial = "";
    private string _apelido = "";
    private double _consumo;

    public MedidorDTO() {}

    public MedidorDTO(string serial, string apelido, double consumo) {
        this._serial = serial;
        this._apelido = serial;
        this._consumo = consumo;
    }
    // private bool isMedidor() 
    // {
    //     List<UsuarioPessoaFisicaDTO<PessoaFisica>> fisica = new List<UsuarioPessoaFisicaDTO<PessoaFisica>>();
    //     Arquivo<Medidor> arquivo = new Arquivo<Medidor>("resources/usuario.txt");
    //     bool success = arquivo.leituraArquivo(fisica);
    //     return success && fisica != null;
    // }

    public string GetApelido() => this._apelido;
    public string GetSerial() => this._serial;

    public double GetConsumo() => this._consumo;
    public void SetApelido(string value) => _apelido = value;
    public void SetSerial(string value) => _serial = value;
    
    public void SetConsumo(double value) => _consumo = value;

}