using DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Confidencial
{
    [Key]
    [Required]
    public string ID { get; set; }

    public string? Nombre { get; set; }
    public string? NombreComercial { get; set; }
    public string? UserSuri { get; set; }
    public string? PassSuri { get; set; }
    public string? UserEftps { get; set; }
    public string? PassEftps { get; set; }
    public string? PIN { get; set; }  // Change to string if database column is string
    public string? UserCFSE { get; set; }
    public string? PassCFSE { get; set; }
    public string? UserDept { get; set; }
    public string? PassDept { get; set; }
    public string? UserCofim { get; set; }
    public string? PassCofim { get; set; }
    public string? UserMunicipio { get; set; }
    public string? PassMunicipio { get; set; }
    public string? CID { get; set; }
    public string? MID { get; set; }

    [ForeignKey("ID")]
    public Registro? Registro { get; set; }
}
