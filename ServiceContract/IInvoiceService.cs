using Exercise_MVC.Models;

namespace MVC_Exercise.ServiceContract
{
    public interface IInvoiceService
    {
        Task<IEnumerable<Invoice>> GetAllInvoices();
        Task<bool> CreateInvoiceAsync(Invoice Invoice);
        Task<Invoice> GetInvoiceByIdAsync(int id);
        Task<bool> DeleteInvoiceAsync(int id);
        Task<bool> UpdateInvoiceAsync(Invoice model);
    }
}
