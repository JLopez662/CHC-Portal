using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Contributivo
    {
        [Key]
        [Required]
        public string ID { get; set; }

        public string? Nombre { get; set; }
        public string? NombreComercial { get; set; }
        public string? Estatal { get; set; }
        public string? Poliza { get; set; }
        public string? RegComerciante { get; set; }
        public DateTime? Vencimiento { get; set; }
        public string? Choferil { get; set; }
        public string? DeptEstado { get; set; }
        public string? CID { get; set; }
        public string? MID { get; set; }

        [ForeignKey("ID")]
        public Registro? Registro { get; set; }
    }
}
