using DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Demografico
{
    [Key]
    [Required]
    public string ID { get; set; }

    [StringLength(100)]
    public string? Nombre { get; set; }

    [StringLength(100)]
    public string? NombreComercial { get; set; }

    [StringLength(200)]
    public string? Dir { get; set; }

    [StringLength(50)]
    public string? Tipo { get; set; }

    [StringLength(50)]
    public string? Patronal { get; set; }

    [StringLength(11)]
    public string? SSN { get; set; }

    [DataType(DataType.Date)]
    public DateTime? Incorporacion { get; set; }

    [DataType(DataType.Date)]
    public DateTime? Operaciones { get; set; }

    [StringLength(100)]
    public string? Industria { get; set; }

    [Range(0, int.MaxValue)]
    public int? NAICS { get; set; }

    [StringLength(500)]
    public string? Descripcion { get; set; }

    [StringLength(100)]
    public string? Contacto { get; set; }

    [Phone]
    public string? Telefono { get; set; }

    [Phone]
    public string? Celular { get; set; }

    [StringLength(200)]
    public string? DirFisica { get; set; }

    [StringLength(200)]
    public string? DirPostal { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    [EmailAddress]
    public string? Email2 { get; set; }

    public string? CID { get; set; }
    public string? MID { get; set; }

    [ForeignKey("ID")]
    public Registro Registro { get; set; }
}
