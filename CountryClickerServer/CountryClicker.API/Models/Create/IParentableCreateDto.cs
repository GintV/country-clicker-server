﻿namespace CountryClicker.API.Models.Create
{
    public interface IParentableCreateDto<TEntity, TParentIdentifier> : ICreateDto<TEntity>
    {
        void SetParentId(TParentIdentifier parentId);
    }
}
