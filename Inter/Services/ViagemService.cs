using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Inter.Data;
using Inter.Models;

namespace Inter.Services
{
    public class ViagemService : IViagemService
    {
        private readonly ApplicationDbContext _context;
        public ViagemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Viagem>> GetViagensAsync()
        {
            var viagens = await _context.Viagem.ToListAsync();
            foreach (Viagem viagem in viagens)
            {
                viagem.Status = await _context.Statuses.FirstOrDefaultAsync(p => p.Id == viagem.StatusId);
            }
            return viagens;
        }
        public async Task<Viagem> GetViagemAsync(int id)
        {
            return await _context.Viagem.FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<bool> SaveViagemAsync(Viagem viagem)
        {

            try
            {
                if (viagem.Id > 0)
                {
                    _context.Update(viagem);
                }
                else
                {
                    _context.Add(viagem);
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

        public async Task<bool> DeleteAsync(Viagem viagem)
        {
            try
            {

                _context.Remove(viagem);

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
