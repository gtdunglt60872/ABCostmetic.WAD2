using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using AutoMapper;

namespace ABCostmeticWAD2.BAL
{
    using DTO;
    using DAL;
    using DAL.Interfaces;
    using DAL.EntityModels;
    using BAL.Interfaces;

    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService()
        {
            _customerRepository = _customerRepository ?? new CustomerRepository();

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Customer, CustomerModel>();
                cfg.CreateMap<CustomerModel, Customer>();
            });

        }

        public IList<CustomerModel> GetAll()
        {
            var list = _customerRepository.GetAll();

            if (list == null || !list.Any())
            {
                return new List<CustomerModel>();
            }

            var res = Mapper.Map<IList<Customer>, IList<CustomerModel>>(list.ToList());

            return res.ToList();
        }

        public CustomerModel GetByKey(int id)
        {
            var model = _customerRepository.FindBy(x => id.ToString().Equals(x.CustomerID)).FirstOrDefault();
            if (model == null)
            {
                return new CustomerModel();
            }

            var res = Mapper.Map<Customer, CustomerModel>(model);
            return res;
        }

        public void Update(CustomerModel obj)
        {
            if (obj == null) return;

            var customer = Mapper.Map<CustomerModel, Customer>(obj);
            _customerRepository.Edit(customer);
            _customerRepository.Save();
        }

        public void Delete(int id)
        {
            if (id == 0)
            {
                return;
            }
            var customerModel = this.GetByKey(id);
            var customer = Mapper.Map<CustomerModel, Customer>(customerModel);
            _customerRepository.Delete(customer);
            _customerRepository.Save();
        }

        public void Add(CustomerModel obj)
        {
            if (obj == null) return;
            var customer = Mapper.Map<CustomerModel, Customer>(obj);
            _customerRepository.Add(customer);
            _customerRepository.Save();
        }
    }
}
