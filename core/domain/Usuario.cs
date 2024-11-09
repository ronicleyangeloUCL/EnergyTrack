using Energytrack.core.domain;
public class Usuario
{
    private string _nome;
    protected List<Medidor> _medidorList = new List<Medidor>();

    public Usuario() { }

    public Usuario(string nome, List<Medidor> medidor)
    {
        this._nome = nome;
        this._medidorList = medidor;
    }

    public Usuario(string nome)
    {
        this._nome = nome;
    }
    public string GetNome() => this._nome;
    public List<Medidor> GetMedidorList() => this._medidorList;

    public void SetNome(string nome)
    {
        this._nome = nome;
    }

    public void SetMedidorList(List<Medidor> medidores)
    {
        this._medidorList = medidores;
    }

    public static void Insert(PessoaJuridica pessoaJuridica)
    {
        string path = "resources/usuario.txt";

        UsuarioPessoaJuridicaDTO dados = new UsuarioPessoaJuridicaDTO(pessoaJuridica);

        Arquivo<UsuarioPessoaJuridicaDTO> arquivo = new Arquivo<UsuarioPessoaJuridicaDTO>(path);

        arquivo.EscritaArquivo("Cadastro de Usuário Pessoa Jurídica", new List<UsuarioPessoaJuridicaDTO> { dados });
    }


}