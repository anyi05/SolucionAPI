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
  public class CursoController : ControllerBase
  {
    private readonly CentroFormacionContext _dbContext;

    public CursoController(CentroFormacionContext dbContext)
    {
      _dbContext = dbContext;
    }

    //Listar Cursos
    [HttpGet]
    [Route("Lista")]
    public async Task<IActionResult> Lista()
    {
      var responseApi = new ResponseAPI<List<CursoDTO>>();
      var listaCursosDTO = new List<CursoDTO>();
      try
      {
        foreach (var item in await _dbContext.Cursos.ToListAsync())
        {
          listaCursosDTO.Add(new CursoDTO
          {
            IdCurso = item.IdCurso,
            Nombre = item.Nombre,
            Descripcion = item.Descripcion,
            Temario = item.Temario,
            Codigo = item.Codigo,
            Creditos = item.Creditos
          });
        }
        responseApi.Success = true;
        responseApi.Data = listaCursosDTO;
      }
      catch (Exception ex)
      {
        responseApi.Success = false;
        responseApi.Message = ex.Message;
      }
      return Ok(responseApi);
    }

    //Buscar un Curso
    [HttpGet]
    [Route("Buscar/{id}")]
    public async Task<IActionResult> Buscar(int id)
    {
      var responseApi = new ResponseAPI<CursoDTO>();
      var CursosDTO = new CursoDTO();
      try
      {
        var dbCurso = await _dbContext.Cursos.FirstOrDefaultAsync(x => x.IdCurso == id);
        if (dbCurso != null)
        {
          CursosDTO.IdCurso = dbCurso.IdCurso;
          CursosDTO.Nombre = dbCurso.Nombre;
          CursosDTO.Descripcion = dbCurso.Descripcion;
          CursosDTO.Temario = dbCurso.Temario;
          CursosDTO.Codigo = dbCurso.Codigo;
          CursosDTO.Creditos = dbCurso.Creditos;

          responseApi.Success = true;
          responseApi.Data = CursosDTO;
        }
        else
        {
          responseApi.Success = false;
          responseApi.Message = "No se encontro el curso";
        }

      }
      catch (Exception ex)
      {
        responseApi.Success = false;
        responseApi.Message = ex.Message;
      }
      return Ok(responseApi);
    }

    //Guardar curso
    [HttpPost]
    [Route("Guardar")]
    public async Task<IActionResult> Guardar(CursoDTO curso)
    {
      var responseApi = new ResponseAPI<int>();
      try
      {
        var dbCurso = new Curso
        {
          Nombre = curso.Nombre,
          Descripcion = curso.Descripcion,
          Temario = curso.Temario,
          Codigo = curso.Codigo,
          Creditos = curso.Creditos
        };
        _dbContext.Cursos.Add(dbCurso);
        await _dbContext.SaveChangesAsync();
        if (dbCurso.IdCurso != 0)
        {
          responseApi.Success = true;
          responseApi.Data = dbCurso.IdCurso;
        }
        else
        {
          responseApi.Success = false;
          responseApi.Message = "Curso no creado";
        }
      }
      catch (Exception ex)
      {
        responseApi.Success = false;
        responseApi.Message = ex.Message;
      }
      return Ok(responseApi);
    }

    //Editar un curso
    [HttpPut]
    [Route("Editar/{id}")]
    public async Task<IActionResult> Editar(CursoDTO curso, int id)
    {
      var responseApi = new ResponseAPI<int>();
      try
      {
        var dbCurso = await _dbContext.Cursos.FirstOrDefaultAsync(c => c.IdCurso == id);

        if (dbCurso != null)
        {
          dbCurso.Nombre = curso.Nombre;
          dbCurso.Descripcion = curso.Descripcion;
          dbCurso.Temario = curso.Temario;
          dbCurso.Codigo = curso.Codigo;
          dbCurso.Creditos = curso.Creditos;

          _dbContext.Cursos.Update(dbCurso);
          await _dbContext.SaveChangesAsync();

          responseApi.Success = true;
          responseApi.Data = dbCurso.IdCurso;
        }
        else
        {
          responseApi.Success = false;
          responseApi.Message = "Curso no encontrado";
        }
      }
      catch (Exception ex)
      {
        responseApi.Success = false;
        responseApi.Message = ex.Message;
      }
      return Ok(responseApi);
    }

    //Eliminar un Curso
    [HttpDelete]
    [Route("Eliminar/{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
      var responseApi = new ResponseAPI<int>();
      try
      {
        var dbCurso = await _dbContext.Cursos.FirstOrDefaultAsync(c => c.IdCurso == id);

        if (dbCurso != null)
        {
          _dbContext.Cursos.Remove(dbCurso);
          await _dbContext.SaveChangesAsync();

          responseApi.Success = true;
        }
        else
        {
          responseApi.Success = false;
          responseApi.Message = "Curso no encontrado";
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
