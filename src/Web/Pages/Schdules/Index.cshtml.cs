using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entites;
using ApplicationCore.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Pages_Schdules
{
    public class IndexModel : PageModel
    {
        private readonly ITvProgramService _tvProgramService;
        private readonly IBroadcastService _broadcastService;
        private readonly IScheduleViewModelService _scheduleViewModelService;

        public IndexModel(ITvProgramService tvProgramService,
                          IBroadcastService broadcastService,
                          IScheduleViewModelService scheduleViewModelService)
        {
            _tvProgramService = tvProgramService;
            _broadcastService = broadcastService;
            _scheduleViewModelService = scheduleViewModelService;
        }

        [BindProperty]
        public int TvProgramId { get; set; }
        [BindProperty]
        public DateTime AirDate { get; set; } = DateTime.Now;

        public SelectList TvPrograms { get; set; }
        public IList<ScheduleIndexViewModel> Schedules { get; set; }
            = Enumerable.Empty<ScheduleIndexViewModel>().ToList();

        public async Task OnGetAsync()
        {
            TvPrograms = new SelectList(await _tvProgramService.GetTvProgramsAsync(),
                                        nameof(TvProgram.Id), nameof(TvProgram.Name));
        }

        public async Task OnPostSearchAsync()
        {
            TvPrograms = new SelectList(await _tvProgramService.GetTvProgramsAsync(),
                                        nameof(TvProgram.Id), nameof(TvProgram.Name));

            var broadcast = await _broadcastService
                .FindBroadcastAsync((int)TvProgramId, AirDate);
            if (broadcast == null)
            {
                await _broadcastService
                    .CreateBroadcastWithDefaultSchedules((int)TvProgramId, AirDate);
            }

            Schedules = await _scheduleViewModelService
                .GetSchedules((int)TvProgramId, AirDate);
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            await _broadcastService
                .DeleteBroadcast((int)TvProgramId, AirDate);

            return RedirectToPage("./Index");
        }
    }
}
