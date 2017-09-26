using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryClicker.API.RoutingParameters
{
    public class PlayerSprintGetResourceRouteParameters : IGetResourceRouteParameters<Guid[]>
    {
        //Interface realization
        public object GetRouteParameters(Guid[] id) => new { playerId = id[0], sprintId = id[1] };
    }
}
