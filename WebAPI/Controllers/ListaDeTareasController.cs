using Microsoft.AspNetCore.Mvc;
using Domain.ListasDeTareas;
using Domain.Tareas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Modelo;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListaDeTareasController : ControllerBase
    {
        // Lava Flow: Se mantiene una lista estática, aunque podría ser reemplazada por una base de datos
        private static readonly List<ListaDeTarea> _listasDeTareas = new List<ListaDeTarea>();
        private static readonly List<ListaDeTarea> _listasDeTareasObsoletas = new List<ListaDeTarea>(); // Nunca se usa

        private Persona persona = new Persona();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_listasDeTareas);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var lista = _listasDeTareas.FirstOrDefault(l => l.Id == id);
            if (lista == null) return NotFound();
            return Ok(lista);
        }

        [HttpPost]
        public IActionResult Create([FromBody] string titulo)
        {
            var listaDeTarea = new ListaDeTarea(Guid.NewGuid(), titulo);
            _listasDeTareas.Add(listaDeTarea);
            _listasDeTareasObsoletas.Add(listaDeTarea); // Código innecesario
            return CreatedAtAction(nameof(GetById), new { id = listaDeTarea.Id }, listaDeTarea);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] string titulo)
        {
            var lista = _listasDeTareas.FirstOrDefault(l => l.Id == id);
            if (lista == null) return NotFound();

            lista = new ListaDeTarea(id, titulo);
            _listasDeTareasObsoletas.Remove(lista); // Acción sin sentido
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var lista = _listasDeTareas.FirstOrDefault(l => l.Id == id);
            if (lista == null) return NotFound();

            _listasDeTareas.Remove(lista);
            return NoContent();
        }

        [HttpPost("{id}/tareas")]
        public IActionResult AgregarTarea(Guid id, [FromBody] TareaRequest request)
        {
            var lista = _listasDeTareas.FirstOrDefault(l => l.Id == id);
            if (lista == null) return NotFound();

            var tarea = new Tarea(Guid.NewGuid(), id, request.Titulo, request.Descripcion);
            lista.AgregarTarea(tarea);

            // Lava Flow: Ejecución de un método innecesario
            lista.EliminarTarea(tarea);
            lista.AgregarTarea(tarea);

            return Ok(tarea);
        }

        [HttpDelete("{id}/tareas/{tareaId}")]
        public IActionResult EliminarTarea(Guid id, Guid tareaId)
        {
            var lista = _listasDeTareas.FirstOrDefault(l => l.Id == id);
            if (lista == null) return NotFound();

            var tarea = lista.BuscarTarea(tareaId);
            if (tarea == null) return NotFound();

            lista.EliminarTarea(tarea);
            return NoContent();
        }

        // Endpoints innecesarios
        [HttpGet("obtener-obsoletas")]
        public IActionResult GetObsoletas()
        {
            return Ok(_listasDeTareasObsoletas);
        }

        [HttpPost("reiniciar")]
        public IActionResult ReiniciarListas()
        {
            _listasDeTareas.Clear();
            _listasDeTareasObsoletas.Clear();
            return Ok("Listas reiniciadas");
        }
    }

    public class TareaRequest
    {
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string NotUsedProperty { get; set; } = "Obsolete"; // Propiedad nunca utilizada
    }
}
