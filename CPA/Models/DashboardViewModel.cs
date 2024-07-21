using DAL.Models;

namespace CPA.Models
{
    public class DashboardViewModel
    {
        public IEnumerable<Demografico>? Demograficos { get; set; }
        public IEnumerable<Contributivo>? Contributivos { get; set; }
        public IEnumerable<Administrativo>? Administrativos { get; set; }
        public IEnumerable<Identificacion>? Identificaciones { get; set; }
        public IEnumerable<Pago>? Pagos { get; set; }
        public IEnumerable<Confidencial>? Confidenciales { get; set; }

        public Demografico NewDemografico { get; set; }
        public Contributivo NewContributivo { get; set; }
        public Administrativo NewAdministrativo { get; set; }
        public Identificacion NewIdentificacion { get; set; }
        public Pago NewPago { get; set; }
        public Confidencial NewConfidencial { get; set; }
    }

}
