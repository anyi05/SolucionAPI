using System;
using System.Collections.Generic;

namespace CentroCrud.Server.Models;

public partial class RecuperacionPassword
{
    public int IdRecPass { get; set; }

    public int UsuarioId { get; set; }

    public string Token { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
