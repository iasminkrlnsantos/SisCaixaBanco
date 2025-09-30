namespace SisCaixaBanco.Models
{
    public class ContaLog
    {
        public int Id { get; set; }
        public int IdContaBancaria { get; set; }
        public string NumeroDocumento { get; set; }
        public DateTime DataDesativacao { get; set; }
        public string UsuarioResponsavel { get; set; }

        public Conta ContaBancaria { get; set; }
    }
}
