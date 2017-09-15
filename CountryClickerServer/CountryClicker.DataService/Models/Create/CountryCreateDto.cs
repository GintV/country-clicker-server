using System;
using System.Collections.Generic;
using System.Text;

namespace CountryClicker.DataService.Models.Create
{
    public class CountryCreateDto
    {
        public string Title { get; set; }
        public Guid ContinentId { get; set; }
    }
}
