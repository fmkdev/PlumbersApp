using System;
using System.Collections.Generic;
using PlumbingService.DTOs;
using PlumbingService.Interfaces.IRepositories;
using PlumbingService.Interfaces.IServices;
using PlumbingService.Models.Entities;

namespace PlumbingService.Implementation.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IJobRepository _jobRepository;

        public CustomerService(ICustomerRepository customerRepository, IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
            _customerRepository = customerRepository;
        }
        public bool Create(CreateCustomerRequestModel model)
        {
            var customer = new Customer
            {
                CustomerPhoto = model.CustomerPhoto,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber
            };
            return _customerRepository.Create(customer);
        }

        
        public void Delete(int id)
        {
            var customer = _customerRepository.Get(id);
            if (customer == null)
            {
                throw new System.Exception("Customer with the Id not found");
            }
            _customerRepository.Delete(customer);
        }

        public CustomerDTO Get(int id)
        {
            var customer = _customerRepository.Get(id);
            if (customer == null)
            {
                throw new System.Exception("Customer with the Id not found");
            }
            return new CustomerDTO
            {
                CustomerPhoto = customer.CustomerPhoto,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Id = customer.Id,
                PhoneNumber = customer.PhoneNumber
            };
        }

        public IEnumerable<CustomerDTO> GetAll()
        {
            return _customerRepository.GetAll();
        }

        public CustomerDTO GetByMail(string email)
        {
            var customer = _customerRepository.GetByMail(email);
            if (customer == null)
            {
                throw new System.Exception("Customer with the Email not found");
            }
            return new CustomerDTO
            {
                CustomerPhoto = customer.CustomerPhoto,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Id = customer.Id,
                PhoneNumber = customer.PhoneNumber
            };
        }
        public CustomerDTO Login(LoginCustomerRequestModel model)
        {
            var customer = _customerRepository.GetByMail(model.Email);
            if(customer == null || customer.Password != model.Password)
            {
                return null;
            }
            return new CustomerDTO
            {
                CustomerPhoto = customer.CustomerPhoto,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Id = customer.Id,
                PhoneNumber = customer.PhoneNumber,
                Address = customer.Address
            };
        }

        public bool Update(UpdateCustomerRequestModel model, int id)
        {
            var customer = _customerRepository.Get(id);
            if (customer == null)
            {
                throw new System.Exception("Customer with the Email not found");
            }
            customer.FirstName = model.FirstName;
            customer.LastName = model.LastName;
            customer.Email = model.Email;
            customer.CustomerPhoto = model.CustomerPhoto;
            customer.PhoneNumber = model.PhoneNumber;

            return _customerRepository.Update(customer);
        }
    }
}