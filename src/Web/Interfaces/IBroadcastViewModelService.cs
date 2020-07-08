using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.Interfaces
{
    public interface IBroadcastViewModelService
    {
        Task<BroadcastViewModel> GetBroadcastViewModel(int tvProgramId, DateTime airDate);
    }
}