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
            
    }
}
