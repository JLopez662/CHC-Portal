using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Empleado
    {
        [Key]
        public string EID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string User { get; set; }
        public string Pass { get; set; }
    }

}
