using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Dominio.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [StringLength(150)]
        public string Descricao { get; set; }

        [Required]
        public decimal Preco { get; set; }

        public decimal PrecoAnterior { get; set; }

        [Column("Categoria")]
        public int IdCategoria { get; set; }

        [JsonIgnore]
        public virtual Categoria Categoria { get;  set; }
    }
}
