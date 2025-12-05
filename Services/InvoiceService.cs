using Exercise_MVC.DBContext;
using Exercise_MVC.Models;
using Microsoft.EntityFrameworkCore;
using MVC_Exercise.ServiceContract;

namespace MVC_Exercise.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ApplicationDbContext _context;
        public InvoiceService(ApplicationDbContext context) { _context = context; }

        public async Task<IEnumerable<Invoice>> GetAllInvoices()
        {
            return await _context.Invoices.ToListAsync();
        }
        public async Task<bool> CreateInvoiceAsync(Invoice Invoice)
        {
            try
            {
                _context.Invoices.Add(Invoice);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> UpdateInvoiceAsync(Invoice model)
        {
            try
            {
                var Invoice = await _context.Invoices.FindAsync(model.InvoiceId);

                if (Invoice == null) {
                    return false;
                }


                _context.Invoices.Update(Invoice);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> DeleteInvoiceAsync(int id)
        {
            try
            {
                var Invoice = await _context.Invoices.FindAsync(id);
                if (Invoice == null)
                {
                    return false;
                }

                _context.Invoices.Remove(Invoice);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<Invoice> GetInvoiceByIdAsync(int id)
        {
            return await _context.Invoices.FirstOrDefaultAsync(p => p.InvoiceId == id);
        }
    }
}
