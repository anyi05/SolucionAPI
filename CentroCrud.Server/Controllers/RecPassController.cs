using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//referencias
using CentroCrud.Server.Models;
using CentroCrud.Shared;
using Microsoft.EntityFrameworkCore;

namespace CentroCrud.Server.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class RecPassController : ControllerBase
  {
    private readonly CentroFormacionContext _dbContext;

    public RecPassController(CentroFormacionContext dbContext)
    {
      _dbContext = dbContext;
    }

    //Listar
    [HttpGet]
    [Route("Lista")]
    public async Task<IActionResult> Lista()
    {
      var responseApi = new ResponseAPI<List<RecPassDTO>>();
      var listaRecPassDTO = new List<RecPassDTO>();
      try
      {
        foreach (var item in await _dbContext.RecuperacionPasswords.ToListAsync())
        {
          listaRecPassDTO.Add(new RecPassDTO
          {
            IdRecPass = item.IdRecPass,
            UsuarioId = item.UsuarioId,
            Token = item.Token,
          });
        }
        responseApi.Success = true;
        responseApi.Data = listaRecPassDTO;
      }
      catch (Exception ex)
      {
        responseApi.Success = false;
        responseApi.Message = ex.Message;
      }
      return Ok(responseApi);
    }

    //Buscar
    [HttpGet]
    [Route("Buscar/{id}")]
    public async Task<IActionResult> Buscar(int id)
    {
      var responseApi = new ResponseAPI<RecPassDTO>();
      var recPassDTO = new RecPassDTO();

      try
      {
        var dbRecPass = await _dbContext.RecuperacionPasswords.FirstOrDefaultAsync(x => x.IdRecPass == id);

        if (dbRecPass != null)
        {
          recPassDTO.IdRecPass = dbRecPass.IdRecPass;
          recPassDTO.UsuarioId = dbRecPass.UsuarioId;
          recPassDTO.Token = dbRecPass.Token;

          responseApi.Success = true;
          responseApi.Data = recPassDTO;
        }
        else
        {
          responseApi.Success = false;
          responseApi.Message = "No se encontro el registro";
        }
      }
      catch (Exception ex)
      {
        responseApi.Success = false;
        responseApi.Message = ex.Message;
      }
      return Ok(responseApi);
    }

    //Guardar
    [HttpPost]
    [Route("Guardar")]
    public async Task<IActionResult> Guardar(RecPassDTO recPassDTO)
    {
      var responseApi = new ResponseAPI<int>();

      try
      {
        var dbRecPass = new RecuperacionPassword
        {
          UsuarioId = recPassDTO.UsuarioId,
          Token = recPassDTO.Token,
        };
        _dbContext.RecuperacionPasswords.Add(dbRecPass);
        await _dbContext.SaveChangesAsync();
        if (dbRecPass.IdRecPass != 0)
        {
          responseApi.Success = true;
          responseApi.Data = dbRecPass.IdRecPass;
        }
        else
        {
          responseApi.Success = false;
          responseApi.Message = "Dato no creado";
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
