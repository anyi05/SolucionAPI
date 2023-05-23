using System;
using System.Collections.Generic;

namespace CentroCrud.Server.Models;

public partial class Roles
{
    public int IdRol { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
