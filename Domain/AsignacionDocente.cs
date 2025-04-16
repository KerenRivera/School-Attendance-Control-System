using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Gestion_de_asistencias
{
    public class AsignacionDocente
    {
        [Key]
        public int IdAsignacionDocente { get; set; }
        public int IdPersona {  get; set; }
        public int idMateria { get; set; }
        public int IdCurso { get; set; }
        public DateTime Horario { get; set; }
        public bool EsTitular { get; set; }
        public Profesor Profesor { get; set; }
        public Materia Materia { get; set; }
        public Curso Curso { get; set; }
       

        public AsignacionDocente(Profesor profesor, Curso curso, Materia materia, DateTime horario, bool esTitular) 
        {
            Profesor = profesor;
            Curso = curso;
            Materia = materia;
            Horario = horario;
            EsTitular = esTitular;
            
        }

        public void MostrarDetalles()
        {
            //jghjjkjhkj
        }

    }
}
