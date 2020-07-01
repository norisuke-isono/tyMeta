using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entites;
using ApplicationCore.Interfaces;
using AutoMapper;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Services
{
    public class ScheduleViewModelService : IScheduleViewModelService
    {
        private readonly IMapper _mapper;
        private readonly IScheduleService _scheduleService;

        public ScheduleViewModelService(IMapper mapper, IScheduleService scheduleService)
        {
            _mapper = mapper;
            _scheduleService = scheduleService;
        }

        public async Task<List<ScheduleIndexViewModel>> GetSchedules(int tvProgramId, DateTime airDate)
        {
            var schedules = await _scheduleService
                .GetSchedulesAsync(tvProgramId, airDate);

            return _mapper.Map<IEnumerable<Schedule>, List<ScheduleIndexViewModel>>(schedules);
        }
    }
}
