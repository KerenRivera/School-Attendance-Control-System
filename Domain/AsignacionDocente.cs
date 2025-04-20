using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Gestion_de_asistencias.Domain
{
    [Table("AsignacionesDocente")]
    public class AsignacionDocente
    {
        [Key]
        public int Id { get; set; }
        public int? IdMateria { get; set; }
        public int? IdCurso { get; set; }
        //public int? IdPersona { get; set; }
        public DateTime Horario { get; set; }
        public bool? EsTitular { get; set; }
        [ForeignKey("IdPersona")]
        public required Maestro Maestro { get; set; }
        [ForeignKey("IdMateria")]
        public required Materia Materia { get; set; }
        [ForeignKey("IdCurso")]
        public required Curso Curso { get; set; }
        //public int IdMaestro { get; internal set; }

        //public AsignacionDocente(Maestro maestro, Curso curso, Materia materia, DateTime horario, bool esTitular)
        //{
        //    Maestro = maestro;
        //    Curso = curso;
        //    Materia = materia;
        //    Horario = horario;
        //    EsTitular = esTitular;
        //}

        public AsignacionDocente()
        {
            
        }
    }
}
