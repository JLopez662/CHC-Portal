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

        public IEnumerable<Demografico> GetDemograficos() => _context.Demograficos.ToList();

        public IEnumerable<Contributivo> GetContributivos() => _context.Contributivos.ToList();

        public IEnumerable<Administrativo> GetAdministrativos() => _context.Administrativos.ToList();

        public IEnumerable<Identificacion> GetIdentificaciones() => _context.Identificaciones.ToList();

        public IEnumerable<Pago> GetPagos() => _context.Pagos.ToList();

        public IEnumerable<Confidencial> GetConfidenciales() => _context.Confidenciales.ToList();

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

        public void CreateDemografico(Demografico demografico)
        {
            _context.Demograficos.Add(demografico);
            _context.SaveChanges();
        }

        public void CreateContributivo(Contributivo contributivo)
        {
            _context.Contributivos.Add(contributivo);
            _context.SaveChanges();
        }

        public void CreateAdministrativo(Administrativo administrativo)
        {
            _context.Administrativos.Add(administrativo);
            _context.SaveChanges();
        }

        public void CreateIdentificacion(Identificacion identificacion)
        {
            _context.Identificaciones.Add(identificacion);
            _context.SaveChanges();
        }

        public void CreatePago(Pago pago)
        {
            _context.Pagos.Add(pago);
            _context.SaveChanges();
        }

        public void CreateConfidencial(Confidencial confidencial)
        {
            _context.Confidenciales.Add(confidencial);
            _context.SaveChanges();
        }

        public void DeleteCustomer(string id)
        {
            var registro = _context.Registros.FirstOrDefault(r => r.ID == id);
            if (registro != null)
            {
                // Delete related Demografico records
                var demograficos = _context.Demograficos.Where(d => d.ID == id);
                _context.Demograficos.RemoveRange(demograficos);

                // Delete related Contributivo records
                var contributivos = _context.Contributivos.Where(c => c.ID == id);
                _context.Contributivos.RemoveRange(contributivos);

                // Delete related Identificacion records
                var identificaciones = _context.Identificaciones.Where(i => i.ID == id);
                _context.Identificaciones.RemoveRange(identificaciones);

                // Delete related Administrativo records
                var administrativos = _context.Administrativos.Where(a => a.ID == id);
                _context.Administrativos.RemoveRange(administrativos);

                // Delete related Pago records
                var pagos = _context.Pagos.Where(p => p.ID == id);
                _context.Pagos.RemoveRange(pagos);

                // Delete related Confidencial records
                var confidenciales = _context.Confidenciales.Where(c => c.ID == id);
                _context.Confidenciales.RemoveRange(confidenciales);

                // Delete Registro record
                _context.Registros.Remove(registro);

                // Save changes
                _context.SaveChanges();
            }
        }
    }
}
