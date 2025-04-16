using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Gestion_de_asistencias
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
        public Alumno Alumno { get; set; }
        public Curso Curso { get; set; }
        public EstadoAsistencia Estado { get; set; }

        public Asistencia(int idAsistencia, Alumno alumno, Curso curso, EstadoAsistencia estado)
        {
            IdAsistencia = idAsistencia;
            Alumno = alumno;
            Curso = curso;
            Estado = estado;
        }

        public void ShowAttendance()
        {
            //jsdjbcfjbj
        }

        public void AddAttendance()
        {
            //jsjdkjs
        }

        public void EditAttendance()
        { 
            //jkfk
        }

        public void DeleteAttendance()
        {
            //jsdkds
        }

    }
}
