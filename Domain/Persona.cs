using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Gestion_de_asistencias.Domain
{
    public abstract class Persona
    {
        [Key]
        public int IdPersona { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Nombre { get; set; }

        [MaxLength(50)]
        public required string Apellido { get; set; }

        public Alumno? Alumno { get; set; }
        public Maestro? Maestro { get; set; }

        protected Persona(int idpersona, string nombre, string apellido)
        {
            IdPersona = idpersona;
            Nombre = nombre;
            Apellido = apellido;
        }

        protected Persona()
        {
        }
    }
}


