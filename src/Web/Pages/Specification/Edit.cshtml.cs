using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.ViewModels;
using Web.Interfaces;

namespace Web.Pages_Specification
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

            return RedirectToPage("/Schedules/Index");
        }
    }
}
