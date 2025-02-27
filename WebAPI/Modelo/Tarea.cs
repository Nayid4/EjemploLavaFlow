using Domain.ListasDeTareas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tareas
{
    public sealed class Tarea
    {
        public Guid Id { get; private set; } = default!;
        public Guid IdListaDetareas { get; private set; } = default!;
        public string Titulo { get; private set; } = string.Empty;
        public string Descripcion { get; private set; } = string.Empty;

        public Tarea() { }

        public Tarea(Guid tareaId, Guid idListaDetareas, string titulo, string descripcion)
        {
            Id = tareaId;
            Titulo = titulo;
            Descripcion = descripcion;
            IdListaDetareas = idListaDetareas;
        }

        public void Actualizar(string titulo, string descripcion)
        {
            Titulo = titulo;
            Descripcion = descripcion;
        }
    }
}
