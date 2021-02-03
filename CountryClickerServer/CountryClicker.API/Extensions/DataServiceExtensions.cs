using System.Linq;
using CountryClicker.API.QueryingParameters;
using CountryClicker.Domain;
using CountryClicker.DataService;

namespace CountryClicker.API.Extensions
{
    public static class DataServiceExtensions
    {
        public static IQueryable<TEntity> GetMany<TEntity, TIdentifier>(this IDataService<TEntity, TIdentifier> dataService,
            BaseResourceParameters baseResourceParameters) where TEntity : class, IEntity =>
            dataService.GetMany().ApplyPaging(baseResourceParameters);

        public static IQueryable<TEntity> GetManyFilter<TEntity, TIdentifier>(this IDataService<TEntity, TIdentifier> dataService,
            BaseResourceParameters baseResourceParameters, params (string column, string value)[] columnValuePairs) where TEntity : class, IEntity =>
            columnValuePairs.Length != 0 ? 
            dataService.GetManyFilter(columnValuePairs).ApplyPaging(baseResourceParameters) :
            dataService.GetMany(baseResourceParameters);

        private static IQueryable<TEntity> ApplyPaging<TEntity>(this IQueryable<TEntity> source, BaseResourceParameters baseResourceParameters) =>
            source.Skip(baseResourceParameters.PageSize * (baseResourceParameters.Page - 1)).Take(baseResourceParameters.PageSize);
    }
}
