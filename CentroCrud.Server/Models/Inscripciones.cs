using System;
using System.Collections.Generic;

namespace CentroCrud.Server.Models;

public partial class Inscripciones
{
    public int IdInscripcion { get; set; }

    public int AlumnoId { get; set; }

    public int CursoId { get; set; }

    public virtual Alumno Alumno { get; set; } = null!;

    public virtual Curso Curso { get; set; } = null!;
}
