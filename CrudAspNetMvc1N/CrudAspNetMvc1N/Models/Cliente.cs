using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CrudAspNetMvc1N.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        
        public string Logotipo { get; set; }

        public virtual List<Logradouro> Logradouros { get; set; }
    }
}