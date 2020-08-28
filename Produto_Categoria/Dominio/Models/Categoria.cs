using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dominio.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Descricao { get; set; }

        [JsonIgnore]
        public virtual ICollection<Produto> Produto { get;  set; }
    }
}
