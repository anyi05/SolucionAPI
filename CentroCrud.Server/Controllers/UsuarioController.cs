using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

//referencias
using CentroCrud.Server.Models;
using CentroCrud.Shared;
using Microsoft.EntityFrameworkCore;

namespace CentroCrud.Server.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UsuarioController : ControllerBase
  {
    private readonly CentroFormacionContext _dbContext;

    public UsuarioController(CentroFormacionContext dbContext)
    {
      _dbContext = dbContext;
    }

    //Listar Usuarios
    [Authorize]
    [HttpGet]
    [Route("Lista")]
    public async Task<IActionResult> Lista()
    {
      var responseApi = new ResponseAPI<List<UsuarioDTO>>();
      var listaUsuariosDTO = new List<UsuarioDTO>();

      try
      {
        foreach (var item in await _dbContext.Usuarios.ToListAsync())
        {
          listaUsuariosDTO.Add(new UsuarioDTO
          {
            IdUsuario = item.IdUsuario,
            Usuario1 = item.Usuario1,
            Password = item.Password,
            Email = item.Email,
            RolId = item.RolId,
            AlumnoId = item.AlumnoId
          });
        }
        responseApi.Success = true;
        responseApi.Data = listaUsuariosDTO;
      }
      catch (Exception ex)
      {
        responseApi.Success = false;
        responseApi.Message = ex.Message;
      }
      return Ok(responseApi);
    }

    //Buscar un Usuario
    [HttpGet]
    [Route("Buscar/{id}")]
    public async Task<IActionResult> Buscar(int id)
    {
      var responseApi = new ResponseAPI<UsuarioDTO>();
      var UsuariosDTO = new UsuarioDTO();

      try
      {
        var item = await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == id);
        if (item != null)
        {
          UsuariosDTO.IdUsuario = item.IdUsuario;
          UsuariosDTO.Usuario1 = item.Usuario1;
          UsuariosDTO.Password = item.Password;
          UsuariosDTO.Email = item.Email;
          UsuariosDTO.RolId = item.RolId;
          UsuariosDTO.AlumnoId = item.AlumnoId;

          responseApi.Success = true;
          responseApi.Data = UsuariosDTO;
          responseApi.Message = "Usuario Encontrado";
        }
        else
        {
          responseApi.Success = false;
          responseApi.Message = "Usuario no Encontrado";
        }
      }
      catch (Exception ex)
      {
        responseApi.Success = false;
        responseApi.Message = ex.Message;
      }
      return Ok(responseApi);
    }

    //Guardar Usuario
    [HttpPost]
    [Route("Guardar")]
    public async Task<IActionResult> Guardar(UsuarioDTO usuario)
    {
      var responseApi = new ResponseAPI<int>();
      try
      {
        var dbUsuario = new Usuario
        {
          IdUsuario = usuario.IdUsuario,
          Usuario1 = usuario.Usuario1,
          Password = usuario.Password,
          Email = usuario.Email,
          RolId = usuario.RolId,
          AlumnoId = usuario.AlumnoId
        };
        _dbContext.Usuarios.Add(dbUsuario);
        await _dbContext.SaveChangesAsync();

        if (dbUsuario.IdUsuario != 0)
        {
          responseApi.Success = true;
          responseApi.Data = dbUsuario.IdUsuario;
        }
        else
        {
          responseApi.Success = false;
          responseApi.Message = "Usuario no creado";
        }
      }
      catch (Exception ex)
      {
        responseApi.Success = false;
        responseApi.Message = ex.Message;
      }
      return Ok(responseApi);
    }

    //Editar un Usuario
    [HttpPut]
    [Route("Editar/{id}")]
    public async Task<IActionResult> Editar(UsuarioDTO usuario, int id)
    {
      var responseApi = new ResponseAPI<int>();

      try
      {
        var dbUsuario = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == id);

        if (dbUsuario != null)
        {
          dbUsuario.Usuario1 = usuario.Usuario1;
          dbUsuario.Password = usuario.Password;
          dbUsuario.Email = usuario.Email;
          dbUsuario.RolId = usuario.RolId;
          dbUsuario.AlumnoId = usuario.AlumnoId;

          _dbContext.Usuarios.Update(dbUsuario);
          await _dbContext.SaveChangesAsync();

          responseApi.Success = true;
          responseApi.Data = dbUsuario.IdUsuario;
        }
        else
        {
          responseApi.Success = false;
          responseApi.Message = "Usuario no encontrado";
        }
      }
      catch (Exception ex)
      {
        responseApi.Success = false;
        responseApi.Message = ex.Message;
      }
      return Ok(responseApi);
    }

    //Eliminar un Usuario
    [HttpDelete]
    [Route("Eliminar/{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
      var responseApi = new ResponseAPI<int>();
      try
      {
        var dbUsuario = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == id);

        if (dbUsuario != null)
        {
          _dbContext.Usuarios.Remove(dbUsuario);
          await _dbContext.SaveChangesAsync();

          responseApi.Success = true;
        }
        else
        {
          responseApi.Success = false;
          responseApi.Message = "Usuario no encontrado";
        }

      }
      catch (Exception ex)
      {
        responseApi.Success = false;
        responseApi.Message = ex.Message;
      }
      return Ok(responseApi);
    }
  }
}
