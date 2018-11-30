using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inter.Models;

namespace Inter.Services
{
    public interface IViagemService
    {
        Task<List<Viagem>> GetViagensAsync();
        Task<Viagem> GetViagemAsync(int id);
        Task<bool> SaveViagemAsync(Viagem viagem);
        Task<bool> DeleteAsync(Viagem viagem);
    }
}
