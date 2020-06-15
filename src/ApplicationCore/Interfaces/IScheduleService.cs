using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entites;

namespace ApplicationCore.Interfaces
{
    public interface IScheduleService
    {
        Task<List<Schedule>> GetSchedulesAsync(int tvProgramId, DateTime airDate);
    }
}