using L01_2019AP650.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2019AP650.Controllers

{
    [Route("api/[controller]")]
    [ApiController]

    public class publicacionesController : ControllerBase
    {
        
      

            private readonly entidadesContext _entidades;

            public publicacionesController(entidadesContext entidad)
            {
                _entidades = entidad;
            }

            //obtener todos los datos

            [HttpGet]
            [Route("GetAll")]

            public IActionResult Get()
            {

                List<publicaciones> listapublicaciones= (from e in _entidades.publicaciones
                                                  select e).ToList();

                if (listapublicaciones.Count() == 0)
                {
                    return NotFound();

                }
                return Ok(listapublicaciones);

            }
            // obtener datos por Id de publicacion
            [HttpGet]
            [Route("GetById/{id}")]

            public IActionResult Get(int id)
            {
                publicaciones? publicacion= (from e in _entidades.publicaciones
                                     where e.publicacionId == id
                                     select e).FirstOrDefault();

                if (publicacion == null)
                {
                    return NotFound();
                }
                return Ok(publicacion);

            }

        //Filtrar publicacion por usuario 

        [HttpGet]
        [Route("find/{filtrousuarioId}")]
        public IActionResult findbyidusuario(int id)
        {
            List<publicaciones> publicacion=(from e in _entidades.publicaciones
                                             where  e.usuarioid== id
                                             select e).ToList();

            if (publicacion.Count==0)
            {
                return NotFound();
            }
            return Ok(publicacion);
        }
        

        // Agregar publicaciones

        [HttpPost]
            [Route("Add")]

            public IActionResult agregarPublicacion([FromBody] publicaciones publicacion)
            {
                try
                {
                    _entidades.publicaciones.Add(publicacion);
                    _entidades.SaveChanges();
                    return Ok(publicacion);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            //actualizar publicaciones por Id
            [HttpPut]
            [Route("actualizar/{id}")]

            public IActionResult modificarpublicacion(int id, [FromBody] publicaciones modificarpublicacion)
            {
                publicaciones? publicactual = (from e in _entidades.publicaciones
                                          where e.publicacionId == id
                                          select e).FirstOrDefault();

                if (publicactual == null)
                {
                    return NotFound();
                }
                publicactual.titulo = modificarpublicacion.titulo;
                publicactual.descripcion = modificarpublicacion.descripcion;
                publicactual.usuarioid = modificarpublicacion.usuarioid;
                
                

                _entidades.Entry(publicactual).State = EntityState.Modified;
                _entidades.SaveChanges();

                return Ok(modificarpublicacion);


            }
            
                //eliminar publicacion

            [HttpDelete]
            [Route("eliminar/{id}")]

            public IActionResult Eliminarpublicacion(int id)
            {

                publicaciones? publicacion = (from e in _entidades.publicaciones
                                     where e.publicacionId == id
                                     select e).FirstOrDefault();

                if (publicacion == null)
                    return NotFound();

                _entidades.publicaciones.Attach(publicacion);
                _entidades.publicaciones.Remove(publicacion);
                _entidades.SaveChanges();

                return Ok(publicacion);
            }

        
    }
}
