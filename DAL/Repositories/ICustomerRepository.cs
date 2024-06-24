using System.Collections.Generic;
using DAL.Models;

namespace DAL.Repositories
{
    public interface ICustomerRepository
    {
        IEnumerable<Demografico> GetDemograficos();
        IEnumerable<Contributivo> GetContributivos();
        IEnumerable<Administrativo> GetAdministrativos();
        IEnumerable<Identificacion> GetIdentificaciones();
        IEnumerable<Pago> GetPagos();
        IEnumerable<Confidencial> GetConfidenciales();
        void UpdateDemografico(Demografico demografico);
    }
}
