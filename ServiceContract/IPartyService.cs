using Exercise_MVC.Models;

namespace MVC_Exercise.ServiceContract
{
    public interface IPartyService
    {
        Task<IEnumerable<Party>> GetAllPartys();
        Task<bool> CreatePartyAsync(Party Party);
        Task<Party> GetPartyByIdAsync(int id);
        Task<bool> DeletePartyAsync(int id);
        Task<bool> UpdatePartyAsync(Party model);
    }
}
