using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
