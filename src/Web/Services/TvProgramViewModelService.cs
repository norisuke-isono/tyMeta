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
    public class TvProgramViewModelService : ITvProgramViewModelService
    {
        private readonly IMapper _mapper;
        private readonly ITvProgramService _tvProgramService;

        public TvProgramViewModelService(IMapper mapper, ITvProgramService tvProgramService)
        {
            _mapper = mapper;
            _tvProgramService = tvProgramService;
        }

        public async Task<TvProgramViewModel> GetTvProgramViewModel(int tvProgramId)
        {
            var tvProgram = await _tvProgramService.FindTvProgramAsync(tvProgramId);

            return _mapper.Map<TvProgram, TvProgramViewModel>(tvProgram);
        }

        public async Task UpdateTvProgram(TvProgramViewModel viewModel)
        {
            var tvProgram = _mapper.Map<TvProgramViewModel, TvProgram>(viewModel);

            await _tvProgramService.UpdateTvProgramAsync(tvProgram);
        }
    }
}
