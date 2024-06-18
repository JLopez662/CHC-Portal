using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using DAL.Repositories;

namespace CPA.DAL.Repositories
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

    }
}
