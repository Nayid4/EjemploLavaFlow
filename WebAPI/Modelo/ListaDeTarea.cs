
using Domain.Tareas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ListasDeTareas
{
    public sealed class ListaDeTarea
    {
        public Guid Id { get; private set; } = default!;
        public string Titulo { get; private set; } = string.Empty;

        private readonly HashSet<Tarea> _tareas = new HashSet<Tarea>();
        public ICollection<Tarea> Tareas => _tareas;

        public ListaDeTarea()
        {
        }

        public ListaDeTarea(Guid idListaDeTareas, string titulo)
        {
            Id = idListaDeTareas;
            Titulo = titulo;
        }

        public void AgregarTarea(Tarea tarea)
        {
            _tareas.Add(tarea);
        }

        public void EliminarTarea(Tarea tarea)
        {
            _tareas.Remove(tarea);
        }

        public Tarea? BuscarTarea(Guid id)
        {
            foreach (var tarea in _tareas )
            {
                if (tarea.Id.Equals(id))
                {
                    return tarea;
                }
            }

            return null;
        }

    }
}
