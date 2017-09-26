using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryClicker.API.RoutingParameters
{
    public class GroupSprintGetResourceRouteParameters : IGetResourceRouteParameters<Guid[]>
    {
        //Interface realization
        public object GetRouteParameters(Guid[] id) => new { groupId = id[0], sprintId = id[1] };
    }
}
