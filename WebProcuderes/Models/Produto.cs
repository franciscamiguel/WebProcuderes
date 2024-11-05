using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProcuderes.Models
{
    [Table("Produto")]
    public class Produto
    {

        [Column("id")]
        [Display(Name = "codigo" )]
        public int Id { get; set; }

        [Column("Nome")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }
    }
}
