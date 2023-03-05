using L01_2019AP650.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2019AP650.Controllers

{
    [Route("api/[controller]")]
    [ApiController]

    public class ComentariosController : ControllerBase
    {



        private readonly entidadesContext _entidades;

        public ComentariosController(entidadesContext entidad)
        {
            _entidades = entidad;
        }

        //obtener todos los datos

        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<Comentarios> listacomentario = (from e in _entidades.comentarios
                                                 select e).ToList();

            if (listacomentario.Count == 0)
            {
                return NotFound();
            }
            return Ok(listacomentario);
        }
        // obtener comentarios
        [HttpGet]
        [Route("GetById/{id}")]

        public IActionResult Get(int id)
        {
            Comentarios? comentario = (from e in _entidades.comentarios
                                       where e.cometarioId == id
                                       select e).FirstOrDefault();

            if (comentario == null)
            {
                return NotFound();
            }
            return Ok(comentario);

        }



        //Filtrar comentarios 

        [HttpGet]
        [Route("find/{filtrocomentarios}")]
        public IActionResult filtro(int id)
        {
            List<Comentarios> comentario= (from e in _entidades.comentarios
                                               where e.publicacionId == id
                                               select e).ToList();

            if (comentario.Count == 0)
            {
                return NotFound();
            }
            return Ok(comentario);
        }


        // Agregar comentarios

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarComentario([FromBody] Comentarios comentario)
        {
            try
            {
                _entidades.comentarios.Add(comentario);
                _entidades.SaveChanges();
                return Ok(comentario);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //actualizar comentarios
        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult modificarcomentarios(int id, [FromBody] Comentarios modificarcomentario)
        {
            Comentarios? comentarioactual = (from e in _entidades.comentarios
                                             where e.cometarioId == id
                                             select e).FirstOrDefault();

            if (comentarioactual == null)
            {
                return NotFound();
            }
           
            comentarioactual.publicacionId = modificarcomentario.publicacionId;
            comentarioactual.comentario = modificarcomentario.comentario;
            comentarioactual.usuarioId = modificarcomentario.usuarioId;



            _entidades.Entry(comentarioactual).State = EntityState.Modified;
            _entidades.SaveChanges();

            return Ok(modificarcomentario);


        }


        //eliminar comentarios

        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult Eliminarcomentario(int id)
        {

            Comentarios? comentario= (from e in _entidades.comentarios
                                          where e.cometarioId == id
                                          select e).FirstOrDefault();

            if (comentario == null)
                return NotFound();

            _entidades.comentarios.Attach(comentario);
            _entidades.comentarios.Remove(comentario);
            _entidades.SaveChanges();

            return Ok(comentario);
        }


    }
}
