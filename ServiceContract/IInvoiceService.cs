using Exercise_MVC.Models;
using MVC_Exercise.Models;

namespace MVC_Exercise.ServiceContract
{
    public interface IInvoiceService
    {
        Task<IEnumerable<InvoiceDTO>> GetAllInvoices();
        Task<bool> CreateInvoiceAsync(Invoice Invoice);
        Task<Invoice> GetInvoiceByIdAsync(int id);
        Task<bool> DeleteInvoiceAsync(int id);
        Task<bool> UpdateInvoiceAsync(Invoice model);
        Task<List<DashboardCount>> GetDashboardCount();
    }
}
