using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.Interfaces
{
    public interface IScheduleViewModelService
    {
        Task<List<ScheduleIndexViewModel>> GetSchedules(int tvProgramId, DateTime airDate);
    }
}