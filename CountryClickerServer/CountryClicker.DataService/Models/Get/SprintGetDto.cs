using CountryClicker.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountryClicker.DataService.Models.Get
{
    public class SprintGetDto : IGetDto<Sprint, Guid>
    {
        public Guid Id { get; set; }
        public long Score { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? FinishTime { get; set; }
    }
}
