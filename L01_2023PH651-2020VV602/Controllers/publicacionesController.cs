using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using L01_2023PH651_2020VV602.Models;

namespace L01_2023PH651_2020VV602.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class publicacionesController : ControllerBase
    {
        private readonly blogDBContext _publicacionesContexto;
        public publicacionesController(blogDBContext publicacionesContexto)
        {
            _publicacionesContexto = publicacionesContexto;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult getAll()
        {
            List<publicaciones> publicacionesLista = (from e in _publicacionesContexto.publicaciones select e).ToList();

            if (publicacionesLista.Count() == 0)
            {
                return NotFound();
            }
            return Ok(publicacionesLista);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult guardarRegistro([FromBody] publicaciones publicaciones)
        {
            try
            {
                _publicacionesContexto.publicaciones.Add(publicaciones);
                _publicacionesContexto.SaveChanges();
                return Ok(publicaciones);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Update/{id}")]
        public IActionResult updateBD(int id, [FromBody] publicaciones modificarPublicaciones)
        {
            publicaciones? publicacionesData = (from e in _publicacionesContexto.publicaciones where e.publicacionId == id select e).FirstOrDefault();

            if (publicacionesData == null)
            {
                return NotFound();
            }

            publicacionesData.titulo = modificarPublicaciones.titulo;
            publicacionesData.descripcion = modificarPublicaciones.descripcion;
            publicacionesData.usuarioId = modificarPublicaciones.usuarioId;

            _publicacionesContexto.Entry(publicacionesData).State = EntityState.Modified;
            _publicacionesContexto.SaveChanges();

            return Ok(modificarPublicaciones);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult delete(int id)
        {
            publicaciones? publicacionesData = (from e in _publicacionesContexto.publicaciones where e.publicacionId == id select e).FirstOrDefault();
            if (publicacionesData == null)
            {
                return NotFound();
            }
            _publicacionesContexto.publicaciones.Attach(publicacionesData);
            _publicacionesContexto.publicaciones.Remove(publicacionesData);
            _publicacionesContexto.SaveChanges();

            return Ok(publicacionesData);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult getById(int id)
        {
            List<publicaciones> publicacionesData = (from e in _publicacionesContexto.publicaciones where e.usuarioId == id select e).ToList();

            if (publicacionesData.Count == 0)
            {
                return NotFound();
            }

            return Ok(publicacionesData);
        }
    }
}
