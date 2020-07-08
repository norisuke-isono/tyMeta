using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entites;

namespace ApplicationCore.Interfaces
{
    public interface IBroadcastService
    {
        Task<Broadcast> FindBroadcastAsync(int tvProgramId, DateTime airDate);

        Task CreateBroadcastWithDefaultSchedulesAsync(int tvProgramId, DateTime airDate);

        Task UpdateBroadcastAsync(Broadcast broadcast);

        Task DeleteBroadcastAsync(int tvProgramId, DateTime airDate);
    }
}