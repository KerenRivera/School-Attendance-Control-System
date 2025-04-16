using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Sistema_de_Gestion_de_asistencias
{
    [Index(nameof(Matricula), IsUnique = true)]
    public class Alumno : Persona
    {
        [Key]
        public int idPersona { get; set; }
        [Required]
        public int Matricula { get; set; }
        public int IdCurso { get; set; } //foreigh key
        public Curso curso { get; set; }
        public Alumno(int idPersona, string nombre, string apellido, int matricula, Curso curso) : base(idPersona, nombre, apellido)
        {
            Matricula = matricula;
            this.curso = curso;
        }

        public void AgregarAlumno(Alumno alumno)
        {
            //hjkjkklkjli
        }

        public void MostrarAlumno(Alumno alumno)
        {
            //jjbjhbjbjbiub
        }
    }

   
}
