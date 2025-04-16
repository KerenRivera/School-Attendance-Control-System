using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Gestion_de_asistencias
{
    public class Materia
    {
        [Key]
        public int IdMateria { get; set; }
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        
        public Materia(int idMateria, string nombre)
        {
            IdMateria = idMateria;
            Nombre = nombre;
        }
    }
}
