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

namespace Web.Pages_TvPrograms
{
    public class DefaultSchedulesModel : PageModel
    {
        private readonly ITvProgramService _tvProgramService;
        private readonly ICornerService _cornerService;
        private readonly ITvProgramViewModelService _tvProgramViewModelService;

        public DefaultSchedulesModel(ITvProgramService tvProgramService,
                                     ICornerService cornerService,
                                     ITvProgramViewModelService tvProgramViewModelService)
        {
            _tvProgramService = tvProgramService;
            _cornerService = cornerService;
            _tvProgramViewModelService = tvProgramViewModelService;
        }

        [BindProperty(SupportsGet = true)]
        public int? TvProgramId { get; set; }

        public SelectList TvPrograms { get; set; }

        public SelectList Corners { get; set; }

        [BindProperty]
        public List<DefaultScheduleViewModel> DefaultScheduleViewModels { get; set; }
            = Enumerable.Empty<DefaultScheduleViewModel>().ToList();

        private async Task SetSelectListAsync()
        {
            TvPrograms = new SelectList(await _tvProgramService.GetTvProgramsAsync(),
                                        nameof(TvProgram.Id), nameof(TvProgram.Name));

            Corners = new SelectList((await _cornerService.GetCornersAsync()).Where(x => x.TvProgramId == TvProgramId),
                                    nameof(Corner.Id), nameof(Corner.Name));
        }

        public async Task OnGetAsync()
        {
            await SetSelectListAsync();

            if (TvProgramId == null) return;

            var tvProgramViewModel = await _tvProgramViewModelService.GetTvProgramViewModel((int)TvProgramId);
            DefaultScheduleViewModels = tvProgramViewModel.DefaultScheduleViewModels.OrderBy(x => x.Sequence).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var tvProgramViewModel = await _tvProgramViewModelService.GetTvProgramViewModel((int)TvProgramId);
            tvProgramViewModel.DefaultScheduleViewModels = DefaultScheduleViewModels
                .Where(x => x.Sequence != 0).ToList(); // WONRFIX: checkboxのhiddenfieldによりsequenceが0のデータが混ざってしまうので除外

            await _tvProgramViewModelService.UpdateTvProgram(tvProgramViewModel);

            return RedirectToPage("/TvPrograms/DefaultSchedules", new { TvProgramId });
        }

        public async Task<IActionResult> OnPostAddDefaultSchedule()
        {
            await SetSelectListAsync();

            DefaultScheduleViewModels.Add(new DefaultScheduleViewModel());

            DefaultScheduleViewModels.Select((s, i) => new { s, i }).ToList()
                .ForEach(x => { x.s.Sequence = x.i + 1; });

            return Partial("_DefaultScheduleRecords", this);
        }

        public async Task<IActionResult> OnPostDeleteDefaultSchedule()
        {
            await SetSelectListAsync();

            DefaultScheduleViewModels = DefaultScheduleViewModels.Where(x => !x.Dead).ToList();

            DefaultScheduleViewModels.Select((s, i) => new { s, i }).ToList()
                .ForEach(x => { x.s.Sequence = x.i + 1; });

            ModelState.Clear();
            return Partial("_DefaultScheduleRecords", this);
        }
    }
}
