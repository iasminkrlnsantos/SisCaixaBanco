using System.ComponentModel.DataAnnotations;

namespace SisCaixaBanco.DTO
{
    public class TransferenciaDTO
    {
         [Required]
         public string DocumentoOrigem { get; set; }
         
         [Required]
         public string DocumentoDestino { get; set; }
         
         [Required]
         [Range(0.00, double.MaxValue)]
         public decimal Valor { get; set; }
         
        
    }
}
