using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Sistema_de_Gestion_de_asistencias.Domain
{
    [Index(nameof(Matricula), IsUnique = true)]
    [Table("Alumnos")]
    public class Alumno : Persona
    {
        //[Key]
        //public int IdPersona { get; set; }
        public int? IdCurso { get; set; }
        public int Matricula { get; set; }

        //[ForeignKey("IdPersona")]
        //public Persona Persona { get; set; } = null!; 

        [ForeignKey("IdCurso")]
        public Curso Curso { get; set; } = null!; 

        public ICollection<Asistencia> Asistencias { get; set; } = new List<Asistencia>();  

        public Alumno()
        { }
    }
}
