using CountryClicker.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountryClicker.DataService.Models.Get
{
    public class CityGetDto : IGetDto<City, Guid>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public long Score { get; set; }
        public Guid CountryId { get; set; }
    }
}
