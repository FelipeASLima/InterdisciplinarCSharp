using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Inter.Data;
using Inter.Models;

namespace Inter.Services
{
    public class StatusService : IStatusService
    {
        private readonly ApplicationDbContext _context;
        public StatusService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Status>> GetStatussAsync()
        {
            return await _context.Statuses.ToListAsync();
        }
        public async Task<Status> GetStatusAsync(int id)
        {
            return await _context.Statuses.FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<bool> SaveStatusAsync(Status status)
        {

            try
            {
                if (status.Id > 0)
                {
                    _context.Update(status);
                }
                else
                {
                    _context.Add(status);
                }
                await _context.SaveChangesAsync();

                return await Task.FromResult(true);
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.

                return await Task.FromResult(false);
            }
        }

        public async Task<bool> DeleteAsync(Status status)
        {
            try
            {

                _context.Remove(status);

                await _context.SaveChangesAsync();

                return await Task.FromResult(true);
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.

                return await Task.FromResult(false);
            }

        }
    }
}
