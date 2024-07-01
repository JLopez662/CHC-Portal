using System.Collections.Generic;
using DAL.Models;

namespace BLL.Services
{
    public interface ICustomerService
    {
        IEnumerable<Demografico> GetDemograficos();
        IEnumerable<Contributivo> GetContributivos();
        IEnumerable<Administrativo> GetAdministrativos();
        IEnumerable<Identificacion> GetIdentificaciones();
        IEnumerable<Pago> GetPagos();
        IEnumerable<Confidencial> GetConfidenciales();
        void UpdateDemografico(Demografico demografico);
        void UpdateContributivo(Contributivo contributivo);
        void UpdateAdministrativo(Administrativo administrativo);
        void UpdateIdentificacion(Identificacion identificacion);
    }
}
