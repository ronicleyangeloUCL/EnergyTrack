using System.Reflection.PortableExecutable;
using System.Text;
using Energytrack.core.domain;

public class UsuarioPessoaFisicaDTO<T> : IUsuario<T>
{
    private PessoaFisica pessoaFisica;
    public UsuarioPessoaFisicaDTO() { }

    public UsuarioPessoaFisicaDTO(PessoaFisica pessoaFisica)
    {
        this.pessoaFisica = pessoaFisica;
    }
    public List<UsuarioPessoaFisicaDTO<PessoaFisica>> UsuarioList()
    {
        List<UsuarioPessoaFisicaDTO<PessoaFisica>> list = new List<UsuarioPessoaFisicaDTO<PessoaFisica>>()
        {
            new UsuarioPessoaFisicaDTO<PessoaFisica>(pessoaFisica)
        };

        return list;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.AppendLine($"Nome: {pessoaFisica.GetNome()};");
        sb.AppendLine($"CPF: {pessoaFisica.GetCpf()};");

        sb.AppendLine("Medidores:");

        for (int i = 0; i < pessoaFisica.GetMedidorList().Count; i++)
        {
            var medidor = pessoaFisica.GetMedidorList()[i];
            sb.AppendLine($"  {i}. Apelido: {medidor.GetApelido()}; Serial: {medidor.GetSerial()};");
        }

        return sb.ToString();
    }
}
