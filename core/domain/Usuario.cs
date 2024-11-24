using Energytrack.core.domain;
public class Usuario
{
    protected string _nome;
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
        string path = "resources/db/usuario.txt";

        UsuarioPessoaJuridicaDTO dados = new UsuarioPessoaJuridicaDTO(pessoaJuridica);

        Arquivo<UsuarioPessoaJuridicaDTO> arquivo = new Arquivo<UsuarioPessoaJuridicaDTO>(path);

        arquivo.EscritaArquivo("", new List<UsuarioPessoaJuridicaDTO> { dados });
    }
    
    public static Usuario SolicitarUsuario(List<Usuario> listUsuario)
    {
        Console.WriteLine("Digite o CPF (Pessoa Física) ou CNPJ (Pessoa Jurídica) do usuário para registrar medições:");
        string identificador = Console.ReadLine().Trim();

        foreach (var usuario in listUsuario)
        {
            if ((usuario is PessoaFisica pf && pf.GetCpf() == identificador) ||
                (usuario is PessoaJuridica pj && pj.GetCnpj() == identificador))
            {
                return usuario;
            }
        }

        return null;
    }


    public static void ExibirUsuario(Usuario usuario)
    {
        if (usuario is PessoaFisica pf)
        {
            Console.WriteLine($"Pessoa Física: {pf.GetNome().ToUpper()}, CPF: {pf.GetCpf()}");
        }
        else if (usuario is PessoaJuridica pj)
        {
            Console.WriteLine($"Pessoa Jurídica: {pj.GetNome().ToUpper()}, CNPJ: {pj.GetCnpj()}");
        }
    }


}