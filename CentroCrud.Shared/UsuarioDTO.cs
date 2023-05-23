using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace CentroCrud.Shared
{
  public class UsuarioDTO
  {
    public int IdUsuario { get; set; }
    public string Usuario1 { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int RolId { get; set; }
    public int? AlumnoId { get; set; }
    public RolDTO? Rol { get; set; }
    public AlumnoDTO? Alumno { get; set; }
  }
}
