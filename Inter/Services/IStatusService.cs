using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inter.Models;

namespace Inter.Services
{
    public interface IStatusService
    {
        Task<List<Status>> GetStatussAsync();
        Task<Status> GetStatusAsync(int id);
        Task<bool> SaveStatusAsync(Status status);
        Task<bool> DeleteAsync(Status status);
    }
}
