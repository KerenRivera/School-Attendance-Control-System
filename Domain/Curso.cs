using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Gestion_de_asistencias.Domain
{
    public class Curso
    {
        [Key]
        public int IdCurso { get; set; }

        public required string Nombre { get; set; }

        public ICollection<Alumno> Alumnos { get; set; } = new List<Alumno>();
        public ICollection<Asistencia> Asistencias { get; set; } = new List<Asistencia>();
        public ICollection<Clase> Clases { get; set; } = new List<Clase>();

        public Curso()
        {
        }
    }

}
