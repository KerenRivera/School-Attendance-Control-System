using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Gestion_de_asistencias
{
    public abstract class Persona
    {
        [Key]
        public int IdPersona { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [StringLength(50)]
        public string Apellido { get; set; }

        public Persona(int idPersona, string nombre, string apellido)
        {
            IdPersona = idPersona;
            Nombre = nombre;
            Apellido = apellido;
        }
    }
}


