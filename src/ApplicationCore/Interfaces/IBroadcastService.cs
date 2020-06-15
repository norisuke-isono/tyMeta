using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entites;

namespace ApplicationCore.Interfaces
{
    public interface IBroadcastService
    {
        Task<Broadcast> FindBroadcastAsync(int tvProgramId, DateTime airDate);

        Task CreateBroadcastWithDefaultSchedules(int tvProgramId, DateTime airDate);

        Task DeleteBroadcast(int tvProgramId, DateTime airDate);
    }
}