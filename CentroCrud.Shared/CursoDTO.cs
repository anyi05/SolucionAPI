using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace CentroCrud.Shared
{
  public class CursoDTO
  {
    public int IdCurso { get; set; }
    [Required(ErrorMessage = "El campo es requerido")]
    public string Nombre { get; set; } = null!;
    public string Descripcion { get; set; } = null!;
    public string Temario { get; set; } = null!;
    public string Codigo { get; set; } = null!;
    [Required(ErrorMessage = "El campo es requerido")]
    public int Creditos { get; set; }
  }
}
