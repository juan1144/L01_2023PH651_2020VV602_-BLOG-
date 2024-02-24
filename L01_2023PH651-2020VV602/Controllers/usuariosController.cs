using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using L01_2023PH651_2020VV602.Models;

namespace L01_2023PH651_2020VV602.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuariosController : ControllerBase
    {
        private readonly blogDBContext _usuariosContexto;
        public usuariosController(blogDBContext usuariosContexto)
        {
            _usuariosContexto = usuariosContexto;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult getAll()
        {
            List<usuarios> usuariosLista = (from e in _usuariosContexto.usuarios select e).ToList();

            if (usuariosLista.Count() == 0)
            {
                return NotFound();
            }
            return Ok(usuariosLista);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult guardarRegistro([FromBody] usuarios usuario)
        {
            try
            {
                _usuariosContexto.usuarios.Add(usuario);
                _usuariosContexto.SaveChanges();
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Update/{id}")]
        public IActionResult updateBD(int id, [FromBody] usuarios modificarUsuarios)
        {
            usuarios? usuariosData = (from e in _usuariosContexto.usuarios where e.usuarioId == id select e).FirstOrDefault();

            if (usuariosData == null)
            {
                return NotFound();
            }

            usuariosData.rolId = modificarUsuarios.rolId;
            usuariosData.nombreUsuario = modificarUsuarios.nombreUsuario;
            usuariosData.clave = modificarUsuarios.clave;
            usuariosData.nombre = modificarUsuarios.nombre;
            usuariosData.apellido = modificarUsuarios.apellido;

            _usuariosContexto.Entry(usuariosData).State = EntityState.Modified;
            _usuariosContexto.SaveChanges();

            return Ok(modificarUsuarios);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult delete(int id)
        {
            usuarios? usuariosData = (from e in _usuariosContexto.usuarios where e.usuarioId == id select e).FirstOrDefault();
            if (usuariosData == null)
            {
                return NotFound();
            }
            _usuariosContexto.usuarios.Attach(usuariosData);
            _usuariosContexto.usuarios.Remove(usuariosData);
            _usuariosContexto.SaveChanges();

            return Ok(usuariosData);
        }

        [HttpGet]
        [Route("FindByName/{filter}")]
        public IActionResult findByName(String filter)
        {
            List<usuarios> usuariosByFilterName = (from e in _usuariosContexto.usuarios where e.nombre.Contains(filter) select e).ToList();
            if (usuariosByFilterName.Count == 0)
            {
                return NotFound();
            }
            return Ok(usuariosByFilterName);
        }

        [HttpGet]
        [Route("FindByLastname/{filter}")]
        public IActionResult findByLastName(String filter)
        {
            List<usuarios> usuariosByFilterLastName = (from e in _usuariosContexto.usuarios where e.apellido.Contains(filter) select e).ToList();
            if (usuariosByFilterLastName.Count == 0)
            {
                return NotFound();
            }
            return Ok(usuariosByFilterLastName);
        }

        [HttpGet]
        [Route("GetByRolId/{id}")]
        public IActionResult getById(int id)
        {
            List<usuarios> usuariosData = (from e in _usuariosContexto.usuarios where e.rolId == id select e).ToList();

            if (usuariosData.Count == 0)
            {
                return NotFound();
            }

            return Ok(usuariosData);
        }
    }
}
