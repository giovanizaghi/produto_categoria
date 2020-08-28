namespace Dominio.Dto.Produto
{
    public class ProdutoPostDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public decimal PrecoAnterior { get; set; }
        public int IdCategoria { get; set; }
    }
}
