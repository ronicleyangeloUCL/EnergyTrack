using Energytrack.core.domain;

public class MedidorDTO 
{
    private string _serial = "";
    private string _apelido = "";

    public MedidorDTO() {}

    public MedidorDTO(string serial, string apelido) {
        this._serial = serial;
        this._apelido = serial;
    }
    private bool isMedidor() 
    {
        List<UsuarioPessoaFisicaDTO<PessoaFisica>> fisica = new List<UsuarioPessoaFisicaDTO<PessoaFisica>>();
        Arquivo<Medidor> arquivo = new Arquivo<Medidor>("resources/usuario.txt");
        bool success = arquivo.leituraArquivo(fisica);
        return success && fisica != null;
    }
}