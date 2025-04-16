using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Gestion_de_asistencias
{
    public class Profesor : Persona
    {
        [Key]
        public int IdPersona { get; set; }
        public Materia IdMateria { get; set; }
        public Profesor(int idPersona, string nombre, string apellido, Materia materia) : base(idPersona, nombre, apellido)
        {
            IdMateria = materia;
        }

        public void MostrarDetalles()
        {
            //nvjhjhjhb
        }

    }
}
