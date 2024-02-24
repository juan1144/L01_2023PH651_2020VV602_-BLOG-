using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using L01_2023PH651_2020VV602.Models;

namespace L01_2023PH651_2020VV602.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class comentariosController : ControllerBase
    {
        private readonly blogDBContext _comentariosContexto;
        public comentariosController(blogDBContext comentariosContexto)
        {
            _comentariosContexto = comentariosContexto;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult getAll()
        {
            List<comentarios> comentariosLista = (from e in _comentariosContexto.comentarios select e).ToList();

            if (comentariosLista.Count() == 0)
            {
                return NotFound();
            }
            return Ok(comentariosLista);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult guardarRegistro([FromBody] comentarios comentarios)
        {
            try
            {
                _comentariosContexto.comentarios.Add(comentarios);
                _comentariosContexto.SaveChanges();
                return Ok(comentarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Update/{id}")]
        public IActionResult updateBD(int id, [FromBody] comentarios modificarComentarios)
        {
            comentarios? comentariosData = (from e in _comentariosContexto.comentarios where e.cometarioId == id select e).FirstOrDefault();

            if (comentariosData == null)
            {
                return NotFound();
            }

            comentariosData.publicacionId = modificarComentarios.publicacionId;
            comentariosData.comentario = modificarComentarios.comentario;
            comentariosData.usuarioId = modificarComentarios.usuarioId;

            _comentariosContexto.Entry(comentariosData).State = EntityState.Modified;
            _comentariosContexto.SaveChanges();

            return Ok(modificarComentarios);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult delete(int id)
        {
            comentarios? comentariosData = (from e in _comentariosContexto.comentarios where e.cometarioId == id select e).FirstOrDefault();
            if (comentariosData == null)
            {
                return NotFound();
            }
            _comentariosContexto.comentarios.Attach(comentariosData);
            _comentariosContexto.comentarios.Remove(comentariosData);
            _comentariosContexto.SaveChanges();

            return Ok(comentariosData);
        }

        [HttpGet]
        [Route("getByPublicacionId/{id}")]
        public IActionResult getByPublicacionId(int id)
        {
            List<comentarios> comentariosData = (from e in _comentariosContexto.comentarios where e.publicacionId == id select e).ToList();

            if (comentariosData.Count == 0)
            {
                return NotFound();
            }

            return Ok(comentariosData);
        }

    }
}
