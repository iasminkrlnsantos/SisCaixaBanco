using System.ComponentModel.DataAnnotations;
using SisCaixaBanco.Resources;

namespace SisCaixaBanco.DTO
{
    public class ContaCreateDTO
    {
        [Required(
            ErrorMessageResourceType = typeof(GlobalResource),
            ErrorMessageResourceName = "LabelObrigatorio"
        )]
        public string Nome { get; set; }


        [Required(
            ErrorMessageResourceType = typeof(GlobalResource),
            ErrorMessageResourceName = "LabelObrigatorio"
        )]
        public string Documento { get; set; }
    }
}
