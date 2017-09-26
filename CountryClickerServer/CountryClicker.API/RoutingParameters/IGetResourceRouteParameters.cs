using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryClicker.API.RoutingParameters
{
    public interface IGetResourceRouteParameters<TIdentifier>
    {
        object GetRouteParameters(TIdentifier id);
    }
}
