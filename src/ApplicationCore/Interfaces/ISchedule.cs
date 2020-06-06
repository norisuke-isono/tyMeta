using System.ComponentModel.DataAnnotations;
using ApplicationCore.Entites;

namespace ApplicationCore.Interfaces
{
    public interface ISchedule
    {
        int Sequence { get; set; }
        Corner Corner { get; set; }
    }
}