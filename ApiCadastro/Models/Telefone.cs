namespace ApiCadastro.Models
{
    public class Telefone
    {
        public int Id { get; set; }
        public int DDD { get; set; }
        public string Numero { get; set; }
        public TipoTelefone Tipo { get; set; }
    }
}
