using System;
using System.Collections.Generic;
using System.Text;

namespace CountryClicker.Domain
{
    public interface IParentableEntity<TParentIdentifier> : IEntity
    {
        TParentIdentifier ParentId(string parentEntityName);
    }
}
