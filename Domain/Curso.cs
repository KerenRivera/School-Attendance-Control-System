using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Gestion_de_asistencias
{
    public class Curso
    {
        [Key]
        public int IdCurso { get; set; }
        [Required]
        [StringLength(25)]
        public string Nombre { get; set; }
        
        public List<Alumno> Alumnos { get; private set; }

        public Curso(int idcurso, string nombre, List<Alumno> alumnos)
        {
            IdCurso = idcurso;
            Nombre = nombre;
            Alumnos = new List<Alumno>();
        }


    }

}
