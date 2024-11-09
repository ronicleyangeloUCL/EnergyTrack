using System.Text;

public class UsuarioPessoaJuridicaDTO
{
    private PessoaJuridica pessoaJuridica;

    public UsuarioPessoaJuridicaDTO(PessoaJuridica pessoaJuridica)
    {
        this.pessoaJuridica = pessoaJuridica;
    }

    public List<UsuarioPessoaJuridicaDTO> UsuarioList()
    {
        List<UsuarioPessoaJuridicaDTO> dados = new List<UsuarioPessoaJuridicaDTO>
        {
            new UsuarioPessoaJuridicaDTO(pessoaJuridica) 
        };

        return dados;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.AppendLine($"Nome da Empresa: {pessoaJuridica.GetNome()}");
        sb.AppendLine($"CNPJ: {pessoaJuridica.GetCnpj()}");

        sb.AppendLine("Medidores:");
        foreach (var medidor in pessoaJuridica.GetMedidorList())
        {
            sb.AppendLine($"  Apelido: {medidor.GetApelido()}, Serial: {medidor.GetSerial()}");
        }

        return sb.ToString();
    }
}
