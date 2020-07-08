using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.Interfaces
{
    public interface ITvProgramViewModelService
    {
        Task<TvProgramViewModel> GetTvProgramViewModel(int tvProgramId);

        Task UpdateTvProgram(TvProgramViewModel viewModel);
    }
}