using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Gestion_de_asistencias.Domain
{
    public class Asistencia
    {
        public enum EstadoAsistencia
        {
            Presente = 'P',
            Ausente = 'A',
            Excusa = 'E',
            Tarde = 'T'
        }

        [Key]
        public int IdAsistencia { get; set; }
        public int IdPersona { get; set; }
        public int IdCurso { get; set; }
        [ForeignKey("IdPersona")]
        public required Alumno Alumno { get; set; }
        [ForeignKey("IdCurso")]
        public required Curso Curso { get; set; }
        [Column(TypeName = "char(1)")]
        public EstadoAsistencia Estado { get; set; }
        public DateTime Fecha { get; set; }

        //public Asistencia(DateTime fecha, Alumno alumno, Curso curso, EstadoAsistencia estado)
        //{
        //    Fecha = fecha;
        //    Alumno = alumno;
        //    Curso = curso;
        //    Estado = estado;
        //}
        //public Asistencia(Alumno alumno, Curso curso, EstadoAsistencia estado)
        //{
        //    Fecha = DateTime.Now;
        //    Alumno = alumno;
        //    Curso = curso;
        //    Estado = estado;
        //}

        public Asistencia()
        { }
    }
}
