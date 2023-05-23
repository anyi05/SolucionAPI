using System;
using System.Collections.Generic;

namespace CentroCrud.Server.Models;

public partial class Curso
{
    public int IdCurso { get; set; }
    public string Nombre { get; set; } = null!;
    public string Descripcion { get; set; } = null!;
    public string Temario { get; set; } = null!;
    public string Codigo { get; set; } = null!;
    public int Creditos { get; set; }
    public virtual ICollection<Inscripciones> Inscripciones { get; set; } = new List<Inscripciones>();
}
