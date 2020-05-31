using System.ComponentModel.DataAnnotations;
using Infrastructure.Models;

namespace Infrastructure.Interfaces
{
    public interface ISchedule
    {
        int Sequence { get; set; }
        Corner Corner { get; set; }
    }
}