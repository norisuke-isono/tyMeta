using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entites;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<TvProgram> TvPrograms { get; set; }
        DbSet<Corner> Corners { get; set; }
        DbSet<Broadcast> Broadcasts { get; set; }
        DbSet<BaseSchedule> BaseSchedules { get; set; }
        DbSet<Schedule> Schedules { get; set; }
        DbSet<Specification> Specifications { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}