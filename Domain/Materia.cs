using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Gestion_de_asistencias.Domain
{
    public class Materia
    {
        [Key]
        public int IdMateria { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Nombre { get; set; }

        public ICollection<Maestro> Maestros { get; set; } = new List<Maestro>();
        public ICollection<AsignacionDocente> Asignaciones { get; set; } = new List<AsignacionDocente>();

        public Materia()
        {
        }
    }
}
