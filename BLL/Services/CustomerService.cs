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

        public IEnumerable<Administrativo> GetAdministrativos()
        {
            return _customerRepository.GetAdministrativos();
        }

        public IEnumerable<Identificacion> GetIdentificaciones()
        {
            return _customerRepository.GetIdentificaciones();
        }

        public IEnumerable<Pago> GetPagos()
        {
            return _customerRepository.GetPagos();
        }

        public IEnumerable<Confidencial> GetConfidenciales()
        {
            return _customerRepository.GetConfidenciales();
        }

        public void UpdateDemografico(Demografico demografico)
        {
            _customerRepository.UpdateDemografico(demografico);
        }

        public void UpdateContributivo(Contributivo contributivo)
        {
            _customerRepository.UpdateContributivo(contributivo);
        }
    }
}
