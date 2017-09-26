using CountryClicker.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountryClicker.DataService.Models.Create
{
    public interface IParentableCreateDto<TEntity, TParentIdentifier> : ICreateDto<TEntity>
    {
        void SetParentId(TParentIdentifier parentId);
    }
}
