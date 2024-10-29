namespace PlataformaClienteFinal.API.Dominio.CreditoConsignado.Aplicacao;

public class IncluirPropostaCommand
{
    public string Cliente { get; set; }
    public decimal Simulacao { get; set; }
    public int Agente { get; set; }
}
