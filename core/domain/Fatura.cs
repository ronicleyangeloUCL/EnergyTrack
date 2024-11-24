using Energytrack.core.domain.Enum;

namespace Energytrack.core.domain;

public class Fatura
{
    private int tipoBandeira;
    private Medidor medidor;

    public Fatura(int tipoBandeira, Medidor medidor)
    {
        this.tipoBandeira = tipoBandeira;
        this.medidor = medidor;
    }
    
    public int GetTipoBandeira() => tipoBandeira;
    public Medidor GetMedidor() => medidor;
    public void SetMedidor(Medidor value) => medidor = value;
    public void SetTipoBandeira(int value) => tipoBandeira = value;


    public void CalcularFatura()
    {
        string path = "resources/db/fatura.txt";
        Arquivo<Medidor> arquivo = new Arquivo<Medidor>();
        
    }
    public void ObterMedidorPrincipal()
    {
        
    }

    public override string ToString()
    {
        return "Fatura " + tipoBandeira;
    }
}