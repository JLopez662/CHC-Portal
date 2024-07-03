using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Identificacion
    {
        [Key]
        public string ID { get; set; }
        public string Nombre { get; set; }
        public string NombreComercial { get; set; }
        public string Accionista { get; set; }
        public string SSNA { get; set; }
        public string Cargo { get; set; }
        public string LicConducir { get; set; }
        public DateTime Nacimiento { get; set; }
        public string? CID { get; set; }
        public string? MID { get; set; }

        [ForeignKey("ID")]
        public Registro Registro { get; set; }
    }
}
