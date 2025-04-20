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
        [Required]
        [MaxLength(25)]
        public required string Nombre { get; set; }

        public ICollection<Alumno> Alumnos { get; set; } = new List<Alumno>();
        public ICollection<Asistencia> Asistencias { get; set; } = new List<Asistencia>();
        public ICollection<AsignacionDocente> Asignaciones { get; set; } = new List<AsignacionDocente>();

        public Curso()
        {
        }
    }

}
