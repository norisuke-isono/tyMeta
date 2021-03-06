using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entites;

namespace ApplicationCore.Interfaces
{
    public interface ITvProgramService
    {
        Task<TvProgram> FindTvProgramAsync(int tvProgramId);

        Task<List<TvProgram>> GetTvProgramsAsync();

        Task UpdateTvProgramAsync(TvProgram tvProgram);
    }
}