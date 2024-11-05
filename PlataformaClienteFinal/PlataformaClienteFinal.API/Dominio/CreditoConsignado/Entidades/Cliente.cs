namespace PlataformaClienteFinal.API.Dominio.CreditoConsignado.Entidades;

public class Cliente
{
    public int Id { get; set; }
    public string Cpf { get; set; }
    public string Rendimento { get; set; }
    public string Endereco { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public bool Bloqueado { get; set; } = false;
}
