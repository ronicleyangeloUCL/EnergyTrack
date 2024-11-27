public class FaturaDTO
{
    private Usuario _usuario;
    private MedidorDTO _medidorDTO;
    private double _custoFatura;
    private int _tipoBandeira;
    public FaturaDTO(Usuario usuario, MedidorDTO medidorDTO, double custoFatura, int tipoBandeira)
    {
        this._usuario = usuario;
        this._medidorDTO = medidorDTO;
        this._custoFatura = custoFatura;
        this._tipoBandeira = tipoBandeira;
    }

    public Usuario GetUsuario() => this._usuario;
    public MedidorDTO GetMedidorDTO() => this._medidorDTO;
    public double GetCustoFatura() => this._custoFatura;
    public int TipoBandeira() => this._tipoBandeira;

    public void SetUsuario(Usuario usuario) => this._usuario = usuario;
    public void SetMedidorDTO(MedidorDTO dto) => this._medidorDTO = dto;
    public void SetCustoFatura(double fatura) => this._custoFatura = fatura;
    public void SetTipoBandeira(int bandeira) => this._tipoBandeira = bandeira;


    public override string ToString()
    {
        // Mapeamento da bandeira para string
        string bandeira = _tipoBandeira switch
        {
            1 => "Bandeira Verde",
            2 => "Bandeira Amarela",
            3 => "Bandeira Vermelha Patamar 1",
            4 => "Bandeira Vermelha Patamar 2",
            _ => "Bandeira Não Definida"
        };

        // Retornando uma string formatada com as informações da fatura
        return $"Fatura: {bandeira} | Usuario: {_usuario.GetNome()} | Medidor: {_medidorDTO.GetApelido()} | Consumo: {_medidorDTO.GetConsumo()} kWh | Custo Fatura: R$ {_custoFatura:F2}";
    }

}