using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Confidencial
    {
        [Key]
        public string ID { get; set; }
        public string Nombre { get; set; }
        public string NombreComercial { get; set; }
        public string UserSuri { get; set; }
        public string PassSuri { get; set; }
        public string UserEftps { get; set; }
        public string PassEftps { get; set; }
        public int PIN { get; set; }
        public string UserCFSE { get; set; }
        public string PassCFSE { get; set; }
        public string UserDept { get; set; }
        public string PassDept { get; set; }
        public string UserCofim { get; set; }
        public string PassCofim { get; set; }
        public string UserMunicipio { get; set; }
        public string PassMunicipio { get; set; }
        public string? CID { get; set; }
        public string? MID { get; set; }

        [ForeignKey("ID")]
        public Registro Registro { get; set; }
    }

}
