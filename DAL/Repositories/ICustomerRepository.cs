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
        void UpdateContributivo(Contributivo contributivo);
        void UpdateAdministrativo(Administrativo administrativo);
        void UpdateIdentificacion(Identificacion identificacion);
        void UpdatePago(Pago pago);
        void UpdateConfidencial(Confidencial confidencial);

        void CreateDemografico(Demografico demografico);
        void CreateContributivo(Contributivo contributivo);
        void CreateAdministrativo(Administrativo administrativo);
        void CreateIdentificacion(Identificacion identificacion);
        void CreatePago(Pago pago);
        void CreateConfidencial(Confidencial confidencial);

        void DeleteCustomer(string id);

    }
}
