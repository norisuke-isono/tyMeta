using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.ViewModels;
using Web.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Pages_Specifications
{
    public class EditModel : PageModel
    {
        private readonly ISpecificationViewModelService _specificationViewModelService;

        public EditModel(ISpecificationViewModelService specificationViewModelService)
        {
            _specificationViewModelService = specificationViewModelService;
        }

        [BindProperty]
        public SpecificationViewModel SpecificationViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            SpecificationViewModel = await _specificationViewModelService
                .GetSpecificationViewModel((int)id);

            if (SpecificationViewModel == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _specificationViewModelService
                .UpdateSpecificationFrom(SpecificationViewModel);

            return RedirectToPage(
                "/Broadcasts/Schedules",
                new
                {
                    AirDate = SpecificationViewModel.AirDate.ToString("yyyy-MM-dd"),
                    TvProgramId = SpecificationViewModel.TvProgramId
                });
        }

        public IActionResult OnPostAddMaterial()
        {
            if (SpecificationViewModel.MaterialSourceViewModels == null)
            {
                SpecificationViewModel.MaterialSourceViewModels =
                    new[] { new SpecificationMaterialSourceViewModel() }.ToList();
            }
            else
            {
                this.SpecificationViewModel.MaterialSourceViewModels
                    .Add(new SpecificationMaterialSourceViewModel());
            }

            return Partial("_MaterialSources", this);
        }

        public IActionResult OnPostDeleteMaterial()
        {
            this.SpecificationViewModel.MaterialSourceViewModels = SpecificationViewModel
                .MaterialSourceViewModels.Where(x => !x.Dead).ToList();

            ModelState.Clear();
            return Partial("_MaterialSources", this);
        }

        public IActionResult OnPostAddInterview()
        {
            if (SpecificationViewModel.InterviewViewModels == null)
            {
                SpecificationViewModel.InterviewViewModels =
                    new[] { new SpecificationInterviewViewModel() }.ToList();
            }
            else
            {
                this.SpecificationViewModel.InterviewViewModels
                    .Add(new SpecificationInterviewViewModel());
            }

            return Partial("_Interviews", this);
        }

        public IActionResult OnPostDeleteInterview()
        {
            this.SpecificationViewModel.InterviewViewModels = SpecificationViewModel
                .InterviewViewModels.Where(x => !x.Dead).ToList();

            ModelState.Clear();
            return Partial("_Interviews", this);
        }

        public IActionResult OnPostAddCast()
        {
            if (SpecificationViewModel.CastViewModels == null)
            {
                SpecificationViewModel.CastViewModels =
                    new[] { new SpecificationCastViewModel() }.ToList();
            }
            else
            {
                this.SpecificationViewModel.CastViewModels
                    .Add(new SpecificationCastViewModel());
            }

            return Partial("_Casts", this);
        }

        public IActionResult OnPostDeleteCast()
        {
            this.SpecificationViewModel.CastViewModels = SpecificationViewModel
                .CastViewModels.Where(x => !x.Dead).ToList();

            ModelState.Clear();
            return Partial("_Casts", this);
        }
    }
}
