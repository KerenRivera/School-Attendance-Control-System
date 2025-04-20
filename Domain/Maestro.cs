using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Gestion_de_asistencias.Domain
{
    public class Maestro: Persona
    {
        //public int IdPersona { get; set; }
        public int? IdMateria { get; set; }

        [ForeignKey("IdMateria")]
        public Materia Materia { get; set; } = null!;

        public Maestro(int idPersona, string nombre, string apellido,  Materia materia) : base(idPersona, nombre, apellido)
        {
            Materia = materia;
            IdMateria = materia.IdMateria;
        }

        public Maestro()
            : base()
        {

        }
    }
}
