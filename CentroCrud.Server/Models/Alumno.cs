using System;
using System.Collections.Generic;

namespace CentroCrud.Server.Models;

public partial class Alumno
{
    public int IdAlumno { get; set; }
    public string Nif { get; set; } = null!;
    public string PrimerNombre { get; set; } = null!;
    public string SegundoNombre { get; set; }
    public string PrimerApellido { get; set; } = null!;
    public string SegundoApellido { get; set; }
    public string Email { get; set; }
    public virtual ICollection<Inscripciones> Inscripciones { get; set; } = new List<Inscripciones>();
    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
