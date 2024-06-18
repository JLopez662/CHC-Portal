using DAL.Models;

namespace CPA.Models
{
    public class DashboardViewModel
    {
        public IEnumerable<Demografico> Demograficos { get; set; }
        public IEnumerable<Contributivo> Contributivos { get; set; }
        public IEnumerable<Administrativo> Administrativos { get; set; }
        public IEnumerable<Identificacion> Identificaciones { get; set; }
        public IEnumerable<Pago> Pagos { get; set; }
        public IEnumerable<Confidencial> Confidenciales { get; set; }

    }
}
