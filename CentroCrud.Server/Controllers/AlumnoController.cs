using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

//referencias
using CentroCrud.Server.Models;
using CentroCrud.Shared;
using Microsoft.EntityFrameworkCore;

namespace CentroCrud.Server.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AlumnoController : ControllerBase
  {
    private readonly CentroFormacionContext _dbContext;

    public AlumnoController(CentroFormacionContext dbContext)
    {
      _dbContext = dbContext;
    }

    //Listar Alumnos
    [HttpGet]
    [Route("Lista")]
    public async Task<IActionResult> Lista()
    {
      var responseApi = new ResponseAPI<List<AlumnoDTO>>();
      var listaAlumnosDTO = new List<AlumnoDTO>();

      try
      {
        foreach (var item in await _dbContext.Alumnos.ToListAsync())
        {
          listaAlumnosDTO.Add(new AlumnoDTO
          {
            IdAlumno = item.IdAlumno,
            Nif = item.Nif,
            PrimerNombre = item.PrimerNombre,
            SegundoNombre = item.SegundoNombre,
            PrimerApellido = item.PrimerApellido,
            SegundoApellido = item.SegundoApellido,
            Email = item.Email
          });
        }
        responseApi.Success = true;
        responseApi.Data = listaAlumnosDTO;
      }
      catch (Exception ex)
      {
        responseApi.Success = false;
        responseApi.Message = ex.Message;
      }
      return Ok(responseApi);
    }

    //Buscar un Alumno
    [HttpGet]
    [Route("Buscar/{id}")]
    public async Task<IActionResult> Buscar(int id)
    {
      var responseApi = new ResponseAPI<AlumnoDTO>();
      var AlumnosDTO = new AlumnoDTO();
      try
      {
        var dbAlumno = await _dbContext.Alumnos.FirstOrDefaultAsync(x => x.IdAlumno == id);

        if (dbAlumno != null)
        {
          AlumnosDTO.IdAlumno = dbAlumno.IdAlumno;
          AlumnosDTO.Nif = dbAlumno.Nif;
          AlumnosDTO.PrimerNombre = dbAlumno.PrimerNombre;
          AlumnosDTO.SegundoNombre = dbAlumno.SegundoNombre;
          AlumnosDTO.PrimerApellido = dbAlumno.PrimerApellido;
          AlumnosDTO.SegundoApellido = dbAlumno.SegundoApellido;
          AlumnosDTO.Email = dbAlumno.Email;

          responseApi.Success = true;
          responseApi.Data = AlumnosDTO;
        }
        else
        {
          responseApi.Success = false;
          responseApi.Message = "Alumno no encontrado";
        }
      }
      catch (Exception ex)
      {
        responseApi.Success = false;
        responseApi.Message = ex.Message;
      }
      return Ok(responseApi);
    }

    //Guardar un Alumno
    [HttpPost]
    [Route("Guardar")]
    public async Task<IActionResult> Guardar(AlumnoDTO alumno)
    {
      var responseApi = new ResponseAPI<int>();
      try
      {
        var dbAlumno = new Alumno
        {
          Nif = alumno.Nif,
          PrimerNombre = alumno.PrimerNombre,
          SegundoNombre = alumno.SegundoNombre,
          PrimerApellido = alumno.PrimerApellido,
          SegundoApellido = alumno.SegundoApellido,
          Email = alumno.Email
        };
        _dbContext.Alumnos.Add(dbAlumno);
        await _dbContext.SaveChangesAsync();

        if (dbAlumno.IdAlumno != 0)
        {
          responseApi.Success = true;
          responseApi.Data = dbAlumno.IdAlumno;
        }
        else
        {
          responseApi.Success = false;
          responseApi.Message = "Alumno no creado";
        }
      }
      catch (Exception ex)
      {
        responseApi.Success = false;
        responseApi.Message = ex.Message;
      }
      return Ok(responseApi);
    }

    //Editar un Alumno
    [HttpPut]
    [Route("Editar/{id}")]
    public async Task<IActionResult> Editar(AlumnoDTO alumno, int id)
    {
      var responseApi = new ResponseAPI<int>();
      try
      {
        var dbAlumno = await _dbContext.Alumnos.FirstOrDefaultAsync(a => a.IdAlumno == id);

        if (dbAlumno != null)
        {
          dbAlumno.Nif = alumno.Nif;
          dbAlumno.PrimerNombre = alumno.PrimerNombre;
          dbAlumno.SegundoNombre = alumno.SegundoNombre;
          dbAlumno.PrimerApellido = alumno.PrimerApellido;
          dbAlumno.SegundoApellido = alumno.SegundoApellido;
          dbAlumno.Email = alumno.Email;

          _dbContext.Alumnos.Update(dbAlumno);
          await _dbContext.SaveChangesAsync();

          responseApi.Success = true;
          responseApi.Data = dbAlumno.IdAlumno;
        }
        else
        {
          responseApi.Success = false;
          responseApi.Message = "Alumno no encontrado";
        }
      }
      catch (Exception ex)
      {
        responseApi.Success = false;
        responseApi.Message = ex.Message;
      }
      return Ok(responseApi);
    }

    //Eliminar un Alumno
    [HttpDelete]
    [Route("Eliminar/{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
      var responseApi = new ResponseAPI<int>();
      try
      {
        var dbAlumno = await _dbContext.Alumnos.FirstOrDefaultAsync(a => a.IdAlumno == id);

        if (dbAlumno != null)
        {
          _dbContext.Alumnos.Remove(dbAlumno);
          await _dbContext.SaveChangesAsync();

          responseApi.Success = true;
        }
        else
        {
          responseApi.Success = false;
          responseApi.Message = "Alumno no encontrado";
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