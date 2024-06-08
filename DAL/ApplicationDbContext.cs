using Azure;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Individuo> Individuos { get; set; }
    public DbSet<Empleado> Empleados { get; set; }
    public DbSet<Registro> Registros { get; set; }
    public DbSet<Demografico> Demograficos { get; set; }
    public DbSet<Contributivo> Contributivos { get; set; }
    public DbSet<Identificacion> Identificaciones { get; set; }
    public DbSet<Administrativo> Administrativos { get; set; }
    public DbSet<Pago> Pagos { get; set; }
    public DbSet<Confidencial> Confidenciales { get; set; }
}
