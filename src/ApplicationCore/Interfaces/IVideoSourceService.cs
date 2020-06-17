using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entites;

namespace ApplicationCore.Interfaces
{
    public interface IVideoSourceService
    {
        Task<List<VideoSource>> GetVideoSourcesAsync();
    }
}