using System;
using System.Collections.Generic;

namespace CentroCrud.Server.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Usuario1 { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int RolId { get; set; }

    public int? AlumnoId { get; set; }

    public virtual Alumno Alumno { get; set; }

    public virtual ICollection<RecuperacionPassword> RecuperacionPasswords { get; set; } = new List<RecuperacionPassword>();

    public virtual Roles Rol { get; set; } = null!;
}
