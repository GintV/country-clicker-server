using System;
using System.Collections.Generic;
using System.Text;

namespace CountryClicker.DataService.Models.Get
{
    public class ContinentGetDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public long Score { get; set; }
    }
}
