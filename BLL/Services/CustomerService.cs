using System.Collections.Generic;
using DAL.Repositories;
using DAL.Models;
using BLL.Services;

namespace BLL
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IEnumerable<Demografico> GetDemograficos()
        {
            return _customerRepository.GetDemograficos();
        }

        public IEnumerable<Contributivo> GetContributivos()
        {
            return _customerRepository.GetContributivos();
        }
    }
}
