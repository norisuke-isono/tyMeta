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
    public class BroadcastViewModelService : IBroadcastViewModelService
    {
        private readonly IMapper _mapper;
        private readonly IBroadcastService _broadcastService;

        public BroadcastViewModelService(IMapper mapper, IBroadcastService broadcastService)
        {
            _mapper = mapper;
            _broadcastService = broadcastService;
        }

        public async Task<BroadcastViewModel> GetBroadcastViewModel(int tvProgramId, DateTime airDate)
        {
            var broadcast = await _broadcastService.FindBroadcastAsync(tvProgramId, airDate);

            return _mapper.Map<Broadcast, BroadcastViewModel>(broadcast);
        }
    }
}
