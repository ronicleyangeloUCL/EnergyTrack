using System.Collections;

class Usuario
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
}