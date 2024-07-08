using System.Collections.Generic;
using DAL.Repositories;
using DAL.Models;
using BLL.Interfaces;

namespace BLL
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IEnumerable<Demografico> GetDemograficos() => _customerRepository.GetDemograficos();
        public IEnumerable<Contributivo> GetContributivos() => _customerRepository.GetContributivos();
        public IEnumerable<Administrativo> GetAdministrativos() => _customerRepository.GetAdministrativos();
        public IEnumerable<Identificacion> GetIdentificaciones() => _customerRepository.GetIdentificaciones();
        public IEnumerable<Pago> GetPagos() => _customerRepository.GetPagos();
        public IEnumerable<Confidencial> GetConfidenciales() => _customerRepository.GetConfidenciales();

        public void UpdateDemografico(Demografico demografico) => _customerRepository.UpdateDemografico(demografico);
        public void UpdateContributivo(Contributivo contributivo) => _customerRepository.UpdateContributivo(contributivo);
        public void UpdateAdministrativo(Administrativo administrativo) => _customerRepository.UpdateAdministrativo(administrativo);
        public void UpdateIdentificacion(Identificacion identificacion) => _customerRepository.UpdateIdentificacion(identificacion);
        public void UpdatePago(Pago pago) => _customerRepository.UpdatePago(pago);
        public void UpdateConfidencial(Confidencial confidencial) => _customerRepository.UpdateConfidencial(confidencial);

        public void CreateDemografico(Demografico demografico) => _customerRepository.CreateDemografico(demografico);
        public void CreateContributivo(Contributivo contributivo) => _customerRepository.CreateContributivo(contributivo);
        public void CreateAdministrativo(Administrativo administrativo) => _customerRepository.CreateAdministrativo(administrativo);
        public void CreateIdentificacion(Identificacion identificacion) => _customerRepository.CreateIdentificacion(identificacion);
        public void CreatePago(Pago pago) => _customerRepository.CreatePago(pago);
        public void CreateConfidencial(Confidencial confidencial) => _customerRepository.CreateConfidencial(confidencial);

        public void DeleteCustomer(string id) => _customerRepository.DeleteCustomer(id);

    }
}
