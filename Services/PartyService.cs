using Exercise_MVC.DBContext;
using Exercise_MVC.Models;
using Microsoft.EntityFrameworkCore;
using MVC_Exercise.ServiceContract;

namespace MVC_Exercise.Services
{
    public class PartyService : IPartyService
    {
        private readonly ApplicationDbContext _context;
        public PartyService(ApplicationDbContext context) { _context = context; }

        public async Task<IEnumerable<Party>> GetAllPartys()
        {
            return await _context.Partys.ToListAsync();
        }
        public async Task<bool> CreatePartyAsync(Party Party)
        {
            try
            {
                _context.Partys.Add(Party);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> UpdatePartyAsync(Party model)
        {
            try
            {
                var Party = await _context.Partys.FindAsync(model.PartyId);

                if (Party == null) {
                    return false;
                }

                Party.PartyName = model.PartyName;

                _context.Partys.Update(Party);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> DeletePartyAsync(int id)
        {
            try
            {
                var Party = await _context.Partys.FindAsync(id);
                if (Party == null)
                {
                    return false;
                }

                _context.Partys.Remove(Party);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<Party> GetPartyByIdAsync(int id)
        {
            return await _context.Partys.FirstOrDefaultAsync(p => p.PartyId == id);
        }
    }
}
