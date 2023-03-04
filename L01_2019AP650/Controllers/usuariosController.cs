using Microsoft.AspNetCore.Http;
using L01_2019AP650.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2019AP650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuariosController : ControllerBase
    {

        private readonly entidades _entidades;
        
        public usuariosController(entidades entidad)
        {
            _entidades = entidad;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get() { 
            
            List<usuarios> listadousuarios =(from e in _entidades.usuarios
                                             select e).ToList();

            if (listadousuarios.Count()==0)
            {
                return NotFound();
                
            }
            return Ok(listadousuarios);

        }
    }
}
