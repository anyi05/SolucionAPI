using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace CentroCrud.Shared
{
  public class InscripcionDTO
  {
    public int IdInscripcion { get; set; }
    public int AlumnoId { get; set; }
    public int CursoId { get; set; }
    public AlumnoDTO? Alumno { get; set; }
    public CursoDTO? Curso { get; set; }
  }
}
