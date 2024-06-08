using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Administrativo
    {
        [Key]
        public string ID { get; set; }
        public string Nombre { get; set; }
        public string NombreComercial { get; set; }
        public DateTime Contrato { get; set; }
        public string Facturacion { get; set; }
        public string FacturacionBase { get; set; }
        public string IVU { get; set; }
        public string Staff { get; set; }
        public DateTime StaffDate { get; set; }
        public string CID { get; set; }
        public string MID { get; set; }

        [ForeignKey("ID")]
        public Registro Registro { get; set; }
    }

}
