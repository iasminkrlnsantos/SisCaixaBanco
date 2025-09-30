using System.ComponentModel.DataAnnotations;

namespace SisCaixaBanco.DTO
{
    public class ContaLogDTO
    {
        [Required]
        public string NumeroDocumento { get; set; }
    }
}
