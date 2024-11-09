public class PessoaJuridica : Usuario
{
    private string _cnpj;
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
        string nomeEmpresa = Console.ReadLine();

        string cnpj = string.Empty;

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

        List<Medidor> listaMedidores = new List<Medidor>();

        string continuar = "S";
        while (continuar.ToUpper() == "S")
        {
            Console.Clear();
            Console.WriteLine("Digite o apelido do medidor:");
            string apelidoMedidor = Console.ReadLine();

            Console.WriteLine("Digite o serial do medidor:");
            string serialMedidor = Console.ReadLine();

            Medidor medidor = new Medidor(apelidoMedidor, serialMedidor);

            // VERIFICAR SE O SERIAL JÁ EXISTE 
            listaMedidores.Add(medidor);
            Console.WriteLine();
            Console.WriteLine("Medidor cadastrado com sucesso!");
            Console.WriteLine("Deseja cadastrar outro medidor? (S/N)");

            continuar = Console.ReadLine()?.ToUpper(); // Garantir que a entrada será maiúscula
            while (continuar != "S" && continuar != "N")
            {
                Console.WriteLine("Erro: Entrada inválida. Digite S para sim ou N para não.");
                continuar = Console.ReadLine()?.ToUpper();
            }
        }
        Console.WriteLine();
        Console.WriteLine("Cadastro finalizado.");
        Console.WriteLine("--------------------");

        Console.WriteLine("Lista de Medidores cadastrados:");
        foreach (var medidor in listaMedidores)
        {
            Console.WriteLine($"Apelido: {medidor.GetApelido()}, Serial: {medidor.GetSerial()}");
        }

        PessoaJuridica pessoaJuridica = new PessoaJuridica();
        pessoaJuridica.SetCnpj(cnpj);
        pessoaJuridica.SetNome(nomeEmpresa);
        pessoaJuridica.SetMedidorList(listaMedidores);

        Insert(pessoaJuridica);

        Console.WriteLine("Cadastro da pessoa jurídica realizado com sucesso!");
    }

    private bool ValidarCNPJ(string cnpj)
    {
        return cnpj.Length == 14 && cnpj.All(char.IsDigit);
    }
}

// OBS POSSO CRIAR UM CLASSE MEDIÇÃO ONDE FICARÁ OS RELATÓRIOS DE CADA CONSUMO 