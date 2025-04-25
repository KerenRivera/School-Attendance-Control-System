using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Gestion_de_asistencias.Domain
{
    [Table("Clases")]
    public class Clase
    {
        [Key]
        public int IdClase { get; set; }
        public int IdMateria { get; set; }
        public int IdCurso { get; set; }
        public DateTime Horario { get; set; }
        public bool? EsTitular { get; set; }
        public required Maestro Maestro { get; set; }
        public required Materia Materia { get; set; }
        public required Curso Curso { get; set; }

        public Clase()
        {
            
        }
    }
}
