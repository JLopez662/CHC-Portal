using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Demografico
    {
        [Key]
        public string ID { get; set; }
        public string Nombre { get; set; }
        public string NombreComercial { get; set; }
        public string Dir { get; set; }
        public string Tipo { get; set; }
        public string Patronal { get; set; }
        public string SSN { get; set; }
        public DateTime Incorporacion { get; set; }
        public DateTime Operaciones { get; set; }
        public string Industria { get; set; }
        public int NAICS { get; set; }
        public string Descripcion { get; set; }
        public string Contacto { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string DirFisica { get; set; }
        public string DirPostal { get; set; }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public string CID { get; set; }
        public string MID { get; set; }

        [ForeignKey("ID")]
        public Registro Registro { get; set; }
    }

}
