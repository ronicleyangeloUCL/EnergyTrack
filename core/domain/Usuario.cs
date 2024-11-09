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

    public static void SalvarCadastroPessoaJuridica(PessoaJuridica pessoaJuridica)
    {
        string path = "resources/usuario.txt";

        UsuarioPessoaJuridicaDTO dados = new UsuarioPessoaJuridicaDTO(pessoaJuridica);

        Arquivo<UsuarioPessoaJuridicaDTO> arquivo = new Arquivo<UsuarioPessoaJuridicaDTO>(path);
        arquivo.EscritaArquivo("Cadastro de Usuário Pessoa Jurídica", dados.UsuarioList());

        Console.WriteLine("Os dados foram salvos no arquivo com sucesso.");
    }

    // public static void Insert(PessoaJuridica pessoaJuridica)
    // {
    //     string path = "resources/usuario.txt";
    //     UsuarioPessoaJuridicaDTO dados = new UsuarioPessoaJuridicaDTO(pessoaJuridica);
    //     Arquivo<UsuarioPessoaJuridicaDTO> arquivo = new Arquivo<UsuarioPessoaJuridicaDTO>(path);
    //     arquivo.EscritaArquivo("Cadastro de Usuário Pessoa Jurídica", dados.UsuarioList());
    // }

    public static void Insert(PessoaJuridica pessoaJuridica)
    {
        string path = "resources/usuario.txt";

        // Cria o DTO para a pessoa jurídica
        UsuarioPessoaJuridicaDTO dados = new UsuarioPessoaJuridicaDTO(pessoaJuridica);

        // Cria uma instância da classe Arquivo
        Arquivo<UsuarioPessoaJuridicaDTO> arquivo = new Arquivo<UsuarioPessoaJuridicaDTO>(path);

        // Escreve os dados no arquivo
        arquivo.EscritaArquivo("Cadastro de Usuário Pessoa Jurídica", new List<UsuarioPessoaJuridicaDTO> { dados });
    }


}