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
  public class InscripcionesController : ControllerBase
  {
    private readonly CentroFormacionContext _dbContext;

    public InscripcionesController(CentroFormacionContext dbContext)
    {
      _dbContext = dbContext;
    }

    //Listar Inscripciones
    [HttpGet]
    [Route("Lista")]
    public async Task<IActionResult> Lista()
    {
      var responseApi = new ResponseAPI<List<InscripcionDTO>>();
      var listaInscripcionesDTO = new List<InscripcionDTO>();
      try
      {
        foreach (var item in await _dbContext.Inscripciones
        //.Include(a => a.Alumno)
        //.Include(c => c.Curso)
        .ToListAsync())
        {
          listaInscripcionesDTO.Add(new InscripcionDTO
          {
            IdInscripcion = item.IdInscripcion,
            AlumnoId = item.AlumnoId,
            CursoId = item.CursoId,
            //Alumno = new AlumnoDTO
            //{
            //  IdAlumno = item.Alumno.IdAlumno,
            //  Nif = item.Alumno.Nif,
            //  PrimerNombre = item.Alumno.PrimerNombre,
            //  SegundoNombre = item.Alumno.SegundoNombre,
            //  PrimerApellido = item.Alumno.PrimerApellido,
            //  SegundoApellido = item.Alumno.SegundoApellido,
            //  Email = item.Alumno.Email
            //},
            //Curso = new CursoDTO
            //{
            //  IdCurso = item.Curso.IdCurso,
            //  Nombre = item.Curso.Nombre,
            //  Descripcion = item.Curso.Descripcion,
            //  Temario = item.Curso.Temario,
            //  Codigo = item.Curso.Codigo,
            //  Creditos = item.Curso.Creditos
            //}
          });
        }
        responseApi.Success = true;
        responseApi.Data = listaInscripcionesDTO;
      }
      catch (Exception ex)
      {
        responseApi.Success = false;
        responseApi.Message = ex.Message;
      }
      return Ok(responseApi);
    }

    //Buscar un Inscripcion
    [HttpGet]
    [Route("Buscar/{id}")]
    public async Task<IActionResult> Buscar(int id)
    {
      var responseApi = new ResponseAPI<InscripcionDTO>();
      var inscripcionDTO = new InscripcionDTO();
      try
      {
        var inscripcion = await _dbContext.Inscripciones
        //.Include(a => a.Alumno)
        //.Include(c => c.Curso)
        .FirstOrDefaultAsync(i => i.IdInscripcion == id);
        if (inscripcion != null)
        {
          inscripcionDTO.IdInscripcion = inscripcion.IdInscripcion;
          inscripcionDTO.AlumnoId = inscripcion.AlumnoId;
          inscripcionDTO.CursoId = inscripcion.CursoId;
          //inscripcionDTO.Alumno = new AlumnoDTO
          //{
          //  IdAlumno = inscripcion.Alumno.IdAlumno,
          //  Nif = inscripcion.Alumno.Nif,
          //  PrimerNombre = inscripcion.Alumno.PrimerNombre,
          //  SegundoNombre = inscripcion.Alumno.SegundoNombre,
          //  PrimerApellido = inscripcion.Alumno.PrimerApellido,
          //  SegundoApellido = inscripcion.Alumno.SegundoApellido,
          //  Email = inscripcion.Alumno.Email
          //};
          //inscripcionDTO.Curso = new CursoDTO
          //{
          //  IdCurso = inscripcion.Curso.IdCurso,
          //  Nombre = inscripcion.Curso.Nombre,
          //  Descripcion = inscripcion.Curso.Descripcion,
          //  Temario = inscripcion.Curso.Temario,
          //  Codigo = inscripcion.Curso.Codigo,
          //  Creditos = inscripcion.Curso.Creditos
          //};
          responseApi.Success = true;
          responseApi.Data = inscripcionDTO;
        }
        else
        {
          responseApi.Success = false;
          responseApi.Message = "No se encontro el inscripcion";
        }
      }
      catch (Exception ex)
      {
        responseApi.Success = false;
        responseApi.Message = ex.Message;
      }
      return Ok(responseApi);
    }

    //Guardar un Inscripcion
    [HttpPost]
    [Route("Guardar")]
    public async Task<IActionResult> Guardar(InscripcionDTO inscripcionDTO)
    {
      var responseApi = new ResponseAPI<int>();
      try
      {
        var dbInscripcion = new Inscripciones
        {
          AlumnoId = inscripcionDTO.AlumnoId,
          CursoId = inscripcionDTO.CursoId
        };

        _dbContext.Inscripciones.Add(dbInscripcion);
        await _dbContext.SaveChangesAsync();

        if (dbInscripcion.IdInscripcion != 0)
        {
          responseApi.Success = true;
          responseApi.Data = dbInscripcion.IdInscripcion;
        }
        else
        {
          responseApi.Success = false;
          responseApi.Message = "No se pudo guardar el inscripcion";
        }
      }
      catch (Exception ex)
      {
        responseApi.Success = false;
        responseApi.Message = ex.Message;
      }
      return Ok(responseApi);
    }

    //Editar un Inscripcion
    [HttpPut]
    [Route("Editar/{id}")]
    public async Task<IActionResult> Editar(int id, InscripcionDTO inscripcionDTO)
    {
      var responseApi = new ResponseAPI<int>();
      try
      {
        var dbInscripcion = await _dbContext.Inscripciones.FirstOrDefaultAsync(i => i.IdInscripcion == id);
        if (dbInscripcion != null)
        {
          dbInscripcion.AlumnoId = inscripcionDTO.AlumnoId;
          dbInscripcion.CursoId = inscripcionDTO.CursoId;

          _dbContext.Inscripciones.Update(dbInscripcion);
          await _dbContext.SaveChangesAsync();

          responseApi.Success = true;
          responseApi.Data = dbInscripcion.IdInscripcion;
        }
        else
        {
          responseApi.Success = false;
          responseApi.Message = "No se encontro el inscripcion";
        }

      }
      catch (Exception ex)
      {
        responseApi.Success = false;
        responseApi.Message = ex.Message;
      }
      return Ok(responseApi);
    }

    //Eliminar un Inscripcion (Registro)
    [HttpDelete]
    [Route("Eliminar/{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
      var responseApi = new ResponseAPI<int>();

      try
      {
        var dbInscripcion = await _dbContext.Inscripciones.FirstOrDefaultAsync(i => i.IdInscripcion == id);

        if (dbInscripcion != null)
        {
          _dbContext.Inscripciones.Remove(dbInscripcion);
          await _dbContext.SaveChangesAsync();

          responseApi.Success = true;
        }
        else
        {
          responseApi.Success = false;
          responseApi.Message = "No se encontro la inscripcion";
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
