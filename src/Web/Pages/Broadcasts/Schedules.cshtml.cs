using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entites;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Pages_Broadcasts
{
    public class SchedulesModel : PageModel
    {
        private readonly ITvProgramService _tvProgramService;
        private readonly IBroadcastService _broadcastService;
        private readonly IBroadcastViewModelService _broadcastViewModelService;

        public SchedulesModel(ITvProgramService tvProgramService,
                              IBroadcastService broadcastService,
                              IBroadcastViewModelService broadcastViewModelService)
        {
            _tvProgramService = tvProgramService;
            _broadcastService = broadcastService;
            _broadcastViewModelService = broadcastViewModelService;
        }

        [BindProperty(SupportsGet = true)]
        public int? TvProgramId { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime AirDate { get; set; } = DateTime.Now;

        public SelectList TvPrograms { get; set; }
        public IList<ScheduleViewModel> ScheduleViewModels { get; set; }
            = Enumerable.Empty<ScheduleViewModel>().ToList();

        public async Task OnGetAsync()
        {
            TvPrograms = new SelectList(await _tvProgramService.GetTvProgramsAsync(),
                                        nameof(TvProgram.Id), nameof(TvProgram.Name));

            if (TvProgramId == null) return;

            var broadcastViewModel = await _broadcastViewModelService
                .GetBroadcastViewModel((int)TvProgramId, AirDate);
            if (broadcastViewModel == null)
            {
                await _broadcastService
                    .CreateBroadcastWithDefaultSchedulesAsync((int)TvProgramId, AirDate);
                broadcastViewModel = await _broadcastViewModelService
                    .GetBroadcastViewModel((int)TvProgramId, AirDate);
            }

            ScheduleViewModels = broadcastViewModel.ScheduleViewModels.OrderBy(x => x.Sequence).ToList();
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            await _broadcastService.DeleteBroadcastAsync((int)TvProgramId, AirDate);

            return RedirectToPage("./Schedules");
        }

        public async Task<IActionResult> OnPostSortAsync(int[] scheduleIds)
        {
            var broadcast = await _broadcastService.FindBroadcastAsync((int)TvProgramId, AirDate);

            broadcast.Schedules.ForEach(x => x.Sequence = Array.IndexOf(scheduleIds, x.Id));

            await _broadcastService.UpdateBroadcastAsync(broadcast);

            return RedirectToPage("./Schedules");
        }
    }
}
