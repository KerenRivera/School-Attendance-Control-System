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
    public class Alumno : Persona
    {
        public int IdCurso { get; set; }
        public int Matricula { get; set; }

        [ForeignKey("IdCurso")]
        public Curso Curso { get; set; } = null!; 

        public ICollection<Asistencia> Asistencias { get; set; } = new List<Asistencia>();  

        public Alumno()
        { }
    }
}
