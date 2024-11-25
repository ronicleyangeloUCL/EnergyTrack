public class Fatura
{
    private int _tipoBandeira;
    private Medidor _medidor;
    private Usuario _usuario;

    public Fatura() { }

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

    public void ProcessarFatura()
    {
        List<Usuario> usuarioList = Arquivo<Usuario>.ProcessarArquivo("usuario");
        Usuario usuarioAtual = Usuario.SolicitarUsuario(usuarioList);

        if (usuarioAtual != null)
        {
            Console.WriteLine($"Usuário encontrado: {usuarioAtual.GetNome()}");
            Usuario.ExibirUsuario(usuarioAtual);
            ObterMedidorPrincipal(usuarioAtual);
        }
        else
        {
            Console.WriteLine("Usuário não encontrado.");
        }
    }

    public void ObterMedidorPrincipal(Usuario usuarioAtual)
    {
        MedidorDTO dto = Arquivo<Medidor>.ProcessarArquivoMedicao("medicao");
        int bandeiraEscolhida = SolicitarBandeiraTarifaria();
        double custoFatura = PrecoFatura(dto, bandeiraEscolhida);
        Console.WriteLine($"RESULTADO FINAL: Apelido: {dto.GetApelido()} | Serial: {dto.GetSerial()} | Consumo: {dto.GetConsumo()} kWh");
        SalvarInformacoesNoArquivo(usuarioAtual, dto, custoFatura, bandeiraEscolhida);
    }

    public int SolicitarBandeiraTarifaria()
    {
        int escolha = 0;
        bool bandeiraValida = false;

        while (!bandeiraValida)
        {
            Console.WriteLine("Escolha a bandeira tarifária:");
            Console.WriteLine("1 - Bandeira Verde");
            Console.WriteLine("2 - Bandeira Amarela");
            Console.WriteLine("3 - Bandeira Vermelha Patamar 1");
            Console.WriteLine("4 - Bandeira Vermelha Patamar 2");

            try
            {
                escolha = int.Parse(Console.ReadLine());
                if (escolha < 1 || escolha > 4)
                {
                    Console.WriteLine("Opção inválida. Por favor, escolha uma opção entre 1 e 4.");
                    continue;
                }
                bandeiraValida = true;
            }
            catch (FormatException)
            {
                Console.WriteLine("Entrada inválida. Por favor, insira um número entre 1 e 4.");
            }
        }

        return escolha;
    }

    public double PrecoFatura(MedidorDTO medidorDto, int tipoBandeira)
    {
        double tarifaBasica = 0.60;
        double custoBandeira = 0.00;

        switch (tipoBandeira)
        {
            case 1:
                Console.WriteLine("Bandeira Verde: Sem custo adicional");
                break;
            case 2:
                Console.WriteLine("Bandeira Amarela: Custo adicional de R$ 0,01 por kWh");
                custoBandeira = 0.01;
                break;
            case 3:
                Console.WriteLine("Bandeira Vermelha Patamar 1: Custo adicional de R$ 0,03 por kWh");
                custoBandeira = 0.03;
                break;
            case 4:
                Console.WriteLine("Bandeira Vermelha Patamar 2: Custo adicional de R$ 0,04 por kWh");
                custoBandeira = 0.04;
                break;
            default:
                Console.WriteLine("Bandeira Tarifária não reconhecida, custo adicional será zero.");
                custoBandeira = 0.00;
                break;
        }

        double custoTotal = (tarifaBasica + custoBandeira) * medidorDto.GetConsumo();
        return custoTotal;
    }

    public void SalvarInformacoesNoArquivo(Usuario usuario, MedidorDTO dto, double custoFatura, int tipoBandeira)
    {
        
        FaturaDTO faturaDTO = new FaturaDTO(usuario, dto, custoFatura,tipoBandeira);
        Insert(faturaDTO);
        // Mapeando a bandeira para string
        // string bandeira = tipoBandeira switch
        // {
        //     1 => "Bandeira Verde",
        //     2 => "Bandeira Amarela",
        //     3 => "Bandeira Vermelha Patamar 1",
        //     4 => "Bandeira Vermelha Patamar 2",
        //     _ => "Bandeira Não Definida"
        // };

        // tranferir do mdedidorDTO para FATURA DTO 
        // using (StreamWriter writer = new StreamWriter(caminhoArquivo, true))
        // {
        //     writer.WriteLine($"Nome: {usuario.GetNome()} | CPF/CNPJ: {usuario.GetIdentificador()} | Serial Medidor: {medidorDto.GetSerial()} | Apelido Medidor: {medidorDto.GetApelido()} | Custo Fatura: R$ {custoFatura:F2} | Bandeira: {bandeira}");
        // }

        // Console.WriteLine("Informações salvas com sucesso no arquivo.");
    }
    

    private void Insert(FaturaDTO dto) 
    {
        string caminhoArquivo = "resources/db/fatura.txt";
        Arquivo<FaturaDTO> arquivo = new Arquivo<FaturaDTO>(caminhoArquivo); 
        arquivo.EscritaArquivo("", new List<FaturaDTO> {dto});
    }


    public override string ToString()
    {
        return "Fatura " + _tipoBandeira;
    }
}
