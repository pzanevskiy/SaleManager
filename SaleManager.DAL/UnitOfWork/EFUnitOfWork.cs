using SaleManager.DAL.Repositories;
using SaleManager.DAL.Repositories.Interfaces;
using SaleManager.DAL.UnitOfWork.Interfaces;
using SaleManager.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SaleManager.DAL
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private SaleContext context;
        private IGenericRepository<Customer> customerRepository;
        private IGenericRepository<Order> orderRepository;
        private IGenericRepository<Product> productRepository;
        private IGenericRepository<Manager> managerRepository;

        public EFUnitOfWork()
        {
            context = new SaleContext();
        }

        public IGenericRepository<Customer> Customers
        {
            get
            {
                if (customerRepository == null)
                {
                    customerRepository = new GenericRepository<Customer>(context);
                }
                return customerRepository;
            }
        }

        public IGenericRepository<Product> Products
        {
            get
            {
                if (productRepository == null)
                {
                    productRepository = new GenericRepository<Product>(context);
                }
                return productRepository;
            }
        }

        public IGenericRepository<Order> Orders
        {
            get
            {
                if (orderRepository == null)
                {
                    orderRepository = new GenericRepository<Order>(context);
                }
                return orderRepository;
            }
        }

        public IGenericRepository<Manager> Managers
        {
            get
            {
                if (managerRepository == null)
                {
                    managerRepository = new GenericRepository<Manager>(context);
                }
                return managerRepository;
            }
        }
        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
