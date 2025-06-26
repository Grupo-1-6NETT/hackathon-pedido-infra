namespace Core.Entities;
public class Item : BaseEntity
{
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public bool Disponivel { get; set; }
    public string NomeCategoria { get; set; } = string.Empty;
}
