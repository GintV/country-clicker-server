using CountryClicker.Data;
using CountryClicker.Domain;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CountryClicker.DataService
{
    public interface IDataService<TEntity, TIdentifier>
        where TEntity : class, IEntity
    {
        EntityEntry Create(TEntity instance);
        void CreateMany(TEntity[] instances);
        EntityEntry Delete(TEntity instance);
        void DeleteMany(TEntity[] instances);
        bool Exists(TIdentifier id);
        TEntity Get(TIdentifier id);
        IQueryable<TEntity> GetMany();
        IQueryable<TEntity> GetManyFilter(params (string column, string value)[] columnValuePair);
        bool AreRelationshipsValid(TEntity instance);
        int SaveChanges();
        EntityEntry Update(TEntity instance);
        void UpdateMany(TEntity[] instances);
    }

    public abstract class DataService<TEntity, TIdentifier> : IDataService<TEntity, TIdentifier>
        where TEntity : class, IEntity
    {
        protected CountryClickerDbContext Context { get; }

        public DataService(CountryClickerDbContext context)
        {
            Context = context;
        }

        public EntityEntry Create(TEntity instance) => Context.Add(instance);
        public void CreateMany(TEntity[] instances) => Context.AddRange(instances);
        public EntityEntry Delete(TEntity instance) => Context.Remove(instance);
        public void DeleteMany(TEntity[] instances) => Context.RemoveRange(instances);
        public bool Exists(TIdentifier id) => Get(id) != null;
        public EntityEntry Update(TEntity instance) => Context.Update(instance);
        public void UpdateMany(TEntity[] instances) => Context.UpdateRange(instances);
        public int SaveChanges() => Context.SaveChanges();

        public abstract TEntity Get(TIdentifier id);
        public abstract IQueryable<TEntity> GetMany();
        public abstract IQueryable<TEntity> GetManyFilter(params (string column, string value)[] columnValuePairs);
        public abstract bool AreRelationshipsValid(TEntity instance);

        protected string CombineFilter((string column, string value)[] columnValuePairs)
        {
            string combinedFilter = string.Empty;
            foreach (var filter in columnValuePairs)
                combinedFilter = $"{combinedFilter} {filter.column} = '{filter.value}' AND";
            return combinedFilter.Substring(0, combinedFilter.Length - 4);
        }
    }
}
