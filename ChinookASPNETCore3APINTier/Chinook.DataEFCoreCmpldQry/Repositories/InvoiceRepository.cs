﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Chinook.DataEFCoreCmpldQry.Repositories
{
    /// <summary>
    /// The invoice repository.
    /// </summary>
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ChinookContext _context;
        
        public InvoiceRepository(ChinookContext context)
        {
            _context = context;
        }

        public InvoiceRepository()
        {
            var services = new ServiceCollection();
            
            var connection = String.Empty;
            
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                connection = "Server=.;Database=Chinook;Trusted_Connection=True;Application Name=ChinookASPNETCoreAPINTier";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                connection = "Server=localhost,1433;Database=Chinook;User=sa;Password=P@55w0rd;Trusted_Connection=False;Application Name=ChinookASPNETCoreAPINTier";
            }

            services.AddDbContextPool<ChinookContext>(options => options.UseSqlServer(connection));
            
            var serviceProvider = services.BuildServiceProvider();

            _context = serviceProvider.GetService<ChinookContext>();
        }

        private bool InvoiceExists(int id) =>
            _context.Invoice.Any(i => i.InvoiceId == id);

        public void Dispose() => _context.Dispose();

        public List<Invoice> GetAll() 
            => _context.GetAllInvoices();

        public Invoice GetById(int id)
        {
            var invoice = _context.GetInvoice(id);
            return invoice;
        }

        public Invoice Add(Invoice newInvoice)
        {
            _context.Invoice.Add(newInvoice);
            _context.SaveChanges();
            return newInvoice;
        }

        public bool Update(Invoice invoice)
        {
            if (!InvoiceExists(invoice.InvoiceId))
                return false;
            _context.Invoice.Update(invoice);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            if (!InvoiceExists(id))
                return false;
            var toRemove = _context.Invoice.Find(id);
            _context.Invoice.Remove(toRemove);
            _context.SaveChanges();
            return true;
        }

        public List<Invoice> GetByEmployeeId(int id) => _context.GetInvoicesByEmployeeId(id);

        public List<Invoice> GetByCustomerId(int id) 
            => _context.GetInvoicesByCustomerId(id);
    }
}