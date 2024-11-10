public class PessoaJuridica : Usuario
{
    private string _cnpj = string.Empty;
    private List<Medidor> _medidorList = new List<Medidor>(); // Declaração e inicialização da lista de medidores

    public PessoaJuridica() { }

    public PessoaJuridica(string cnpj, string nome) : base(nome)
    {
        this._cnpj = cnpj;
    }

    public void SetCnpj(string cnpj) => _cnpj = cnpj;
    public string GetCnpj() => _cnpj;

    public void AdicionarMedidor(Medidor medidor)
    {
        _medidorList.Add(medidor);
    }

    public void CadastroUsuarioPessoaJuridica()
    {
        Console.Clear();
        Console.WriteLine("---------------------------");
        Console.WriteLine("Cadastro de Pessoa Jurídica");
        Console.WriteLine("---------------------------");

        Console.WriteLine("Informe o nome da empresa:");
        string? nomeEmpresa = Console.ReadLine();

        string? cnpj = string.Empty;

        while (true)
        {
            Console.WriteLine("Informe o CNPJ da empresa:");
            cnpj = Console.ReadLine();

            if (ValidarCNPJ(cnpj))
            {
                break;
            }
            else
            {
                Console.WriteLine("CNPJ inválido. Tente novamente.");
            }
        }

        Console.WriteLine();
        Console.WriteLine("---------------------------");
        Console.WriteLine("Cadastro de Medidores");
        Console.WriteLine("---------------------------");

        List<Medidor> listaMedidores = Medidor.CadastroMedidor();

        Console.WriteLine();
        Console.WriteLine("Cadastro finalizado.");
        Console.WriteLine("--------------------");

        Console.WriteLine("Lista de Medidores cadastrados:");
        foreach (var medidor in listaMedidores)
        {
            Console.WriteLine($"Apelido: {medidor.GetApelido()}, Serial: {medidor.GetSerial()}");
        }

        PessoaJuridica pessoaJuridica = new PessoaJuridica();
        if ((cnpj != null) && (nomeEmpresa != null))
        {
            pessoaJuridica.SetCnpj(cnpj);
            pessoaJuridica.SetNome(nomeEmpresa);
        }

        pessoaJuridica.SetMedidorList(listaMedidores);

        Insert(pessoaJuridica);

        Console.WriteLine("Cadastro da pessoa jurídica realizado com sucesso!");
    }

    private bool ValidarCNPJ(string cnpj)
    {
        return cnpj.Length == 14 && cnpj.All(char.IsDigit);
    }

    private string isValidtedCNPJ()
    {
        string? cnpj = string.Empty;

        while (true)
        {
            Console.WriteLine("Informe o CNPJ da empresa:");
            cnpj = Console.ReadLine();

            if (ValidarCNPJ(cnpj))
            {
                break;
            }
            else
            {
                Console.WriteLine("CNPJ inválido. Tente novamente.");
            }
        }
        return cnpj;
    }
}

// OBS POSSO CRIAR UM CLASSE MEDIÇÃO ONDE FICARÁ OS RELATÓRIOS DE CADA CONSUMO 