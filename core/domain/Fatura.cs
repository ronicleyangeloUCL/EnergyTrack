using Energytrack.core.domain.Enum;

namespace Energytrack.core.domain;

public class Fatura
{
    private int _tipoBandeira;
    private Medidor _medidor;
    private Usuario _usuario;
    public Fatura() {}
    public Fatura(int tipoBandeira, Medidor medidor)
    {
        this._tipoBandeira = tipoBandeira;
        this._medidor = medidor;
    }
    
    public int GetTipoBandeira() => _tipoBandeira;
    public Medidor GetMedidor() => _medidor;
    public Usuario GetUsuario() => _usuario;
    public void SetMedidor(Medidor value) => _medidor = value;
    public void SetTipoBandeira(int value) => _tipoBandeira = value;
    public void SetUsuario(Usuario value) => _usuario = value;

    public void CalcularFatura()
    {
        List<Usuario> usuarioList = Arquivo<Usuario>.ProcessarArquivo("usuario".ToLower());
        Console.WriteLine("lista de usuario", usuarioList);
        Usuario usuarioAtual = Usuario.SolicitarUsuario(usuarioList);
        
        if (usuarioAtual != null)
        {
            Console.WriteLine("usuario no encontrado", usuarioAtual.GetNome());
            Usuario.ExibirUsuario(usuarioAtual);
            ObterMedidorPrincipal();
        }
    }
    public void ObterMedidorPrincipal()
    {
        MedidorDTO dto = Arquivo<Medidor>.ProcessarArquivoMedicao("medicao".ToLower());
        Console.WriteLine($"RESULTADO FINAL {dto.GetApelido()} | {dto.GetSerial()} | {dto.GetConsumo()}");
    }

    public override string ToString()
    {
        return "Fatura " + _tipoBandeira;
    }
}