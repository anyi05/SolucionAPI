using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentroCrud.Shared
{
  public class RecPassDTO
  {
    public int IdRecPass { get; set; }
    public int UsuarioId { get; set; }
    public string Token { get; set; } = null!;
    public UsuarioDTO? Usuario { get; set; }
  }
}
