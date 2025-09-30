using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SisCaixaBanco.Common.Enums
{
    public enum StatusConta
    {
        [Display(Name="Ativa")]
        Ativa = 1,
        [Display(Name = "Ativa")]
        Inativa = 0
    }
}
