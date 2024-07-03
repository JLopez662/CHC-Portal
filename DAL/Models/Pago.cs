using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Pago
    {
        [Key]
        public string ID { get; set; }
        public string Nombre { get; set; }
        public string NombreComercial { get; set; }
        public string BankClient { get; set; }
        public string Banco { get; set; }
        public string NumRuta { get; set; }
        public string NameBank { get; set; }
        public string TipoCuenta { get; set; }
        public string BankClientS { get; set; }
        public string BancoS { get; set; }
        public string NumRutaS { get; set; }
        public string NameBankS { get; set; }
        public string TipoCuentaS { get; set; }
        public string NameCard { get; set; }
        public string Tarjeta { get; set; }
        public string TipoTarjeta { get; set; }
        public string CVV { get; set; }
        public DateTime Expiracion { get; set; }
        public string PostalBank { get; set; }
        public string NameCardS { get; set; }
        public string TarjetaS { get; set; }
        public string TipoTarjetaS { get; set; }
        public string CVVS { get; set; }
        public DateTime ExpiracionS { get; set; }
        public string PostalBankS { get; set; }
        public string? CID { get; set; }
        public string? MID { get; set; }

        [ForeignKey("ID")]
        public Registro Registro { get; set; }
    }

}
