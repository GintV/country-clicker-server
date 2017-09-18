using System;
using System.Collections.Generic;
using System.Text;

namespace CountryClicker.DataService.Models.Get
{
    public interface IGetableDto<TIdentifier>
    {
        TIdentifier Id { get; set; }
    }
}
