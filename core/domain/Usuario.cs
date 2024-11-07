using System.Collections;
using System.ComponentModel.DataAnnotations;
using Energytrack.core.domain;
using Energytrack.core.DTO;

public class Usuario 
{
    private string _nome;
    private List<Medidor> _medidorList = new List<Medidor>();

    public Usuario() {}

    public Usuario(string nome, List<Medidor> medidor)
    {
        this._nome = nome;
        this._medidorList = medidor;
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

    public void cadastroUsuarioPessoaFisica()
    {
        Console.WriteLine("------------------");
        Console.WriteLine("Cadastro de Pessoa Física");
        Console.WriteLine("------------------");
        Console.WriteLine("Informe o seu nome");
        _nome = Console.ReadLine();
        
    }

    public void cadastroUsuarioPessoaJuridica()
    {
        PessoaJuridica pessoaJuridica = new PessoaJuridica();
        
        Console.WriteLine("---------------------------");
        Console.WriteLine("Cadastro de Pessoa Jurídica");
        Console.WriteLine("---------------------------");
        Console.WriteLine("Informe o seu nome");
        string name = Console.ReadLine();


        Console.WriteLine("---------------------");
        Console.WriteLine("Informe o seu medidor");
        Console.WriteLine("---------------------");

        Medidor listaMedidor = new Medidor();
        Console.WriteLine("Digite o apelido do medidor");
        string apelido = Console.ReadLine();

        Console.WriteLine("Digite o serial do medidor");
        string serial = Console.ReadLine();

        listaMedidor.SetApelido(apelido);
        listaMedidor.SetSerial(serial);


        string path = "resources/usuario";
        UsuarioPessoaJuridicaDTO dados = new UsuarioPessoaJuridicaDTO(_nome, apelido, serial);
        Console.WriteLine(dados.usuarioList());
        Arquivo arquivo = new Arquivo(path);
        // arquivo.EscritaArquivo("Cadastro de Usuario", dados);
    }
}