using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Services
{
    public class ScheduleViewModelService : IScheduleViewModelService
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleViewModelService(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        public async Task<List<ScheduleIndexViewModel>> GetSchedules(int tvProgramId, DateTime airDate)
        {
            var schedules = await _scheduleService.GetSchedulesAsync(tvProgramId, airDate);

            var viewModels = schedules.Select(schedule =>
                new ScheduleIndexViewModel
                {
                    Sequence = schedule.Sequence,
                    CornerName = schedule.Corner.Name,
                    CornerType = schedule.Corner.Type,
                    AirDate = schedule.Broadcast.AirDate,
                    Title = schedule.Specification.Title,
                    ScheduleId = schedule.Id,
                    BroadcastId = schedule.BroadcastId,
                    CornerId = schedule.Corner.Id,
                    SpecificationId = schedule.Specification.Id,
                }).ToList();

            return viewModels;
        }
    }
}
