using System;
using CountryClicker.Domain;

namespace CountryClicker.API.Models.Get
{
    public class CustomGroupGetDto : IGetDto<CustomGroup, Guid>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public long Score { get; set; }
        public Guid CreatedById { get; set; }
    }
}
