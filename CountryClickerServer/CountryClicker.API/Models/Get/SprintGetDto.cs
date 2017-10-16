using System;
using CountryClicker.Domain;

namespace CountryClicker.API.Models.Get
{
    public class SprintGetDto : IGetDto<Sprint, Guid>
    {
        public Guid Id { get; set; }
        public long Score { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? FinishTime { get; set; }
    }
}
