using System;
using System.Collections.Generic;
using System.Text;

namespace CountryClicker.DataService.Models.Get
{
    public interface IGetDto<TEntity, TIdentifier>
    {
        TIdentifier Id { get; }
    }
}
