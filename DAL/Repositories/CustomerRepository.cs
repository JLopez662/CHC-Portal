using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Demografico> GetDemograficos()
        {
            return _context.Demograficos.ToList();
        }

        public IEnumerable<Contributivo> GetContributivos()
        {
            return _context.Contributivos.ToList();
        }

        public IEnumerable<Administrativo> GetAdministrativos()
        {
            return _context.Administrativos.ToList();
        }

        public IEnumerable<Identificacion> GetIdentificaciones()
        {
            return _context.Identificaciones.ToList();
        }

        public IEnumerable<Pago> GetPagos()
        {
            return _context.Pagos.ToList();
        }

        public IEnumerable<Confidencial> GetConfidenciales()
        {
            return _context.Confidenciales.ToList();
        }

        public void UpdateDemografico(Demografico demografico)
        {
            _context.Entry(demografico).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void UpdateContributivo(Contributivo contributivo)
        {
            _context.Entry(contributivo).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void UpdateAdministrativo(Administrativo administrativo)
        {
            _context.Entry(administrativo).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void UpdateIdentificacion(Identificacion identificacion)
        {
            _context.Entry(identificacion).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void UpdatePago(Pago pago)
        {
            _context.Entry(pago).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void UpdateConfidencial(Confidencial confidencial)
        {
            _context.Entry(confidencial).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
