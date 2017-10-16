using System;
using CountryClicker.Domain;

namespace CountryClicker.API.Models.Get
{
    public class CityGetDto : IGetDto<City, Guid>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public long Score { get; set; }
        public Guid CountryId { get; set; }
    }
}
