using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCadastro.Models
{
    public class Logradouro
    {
        public int Id { get; set; }
        [Column("Endereço")]
        public string Endereco { get; set; }
    }
}
