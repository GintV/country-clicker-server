using CountryClicker.Data;
using CountryClicker.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountryClicker.DataService
{
    public class CountryDataService : DataService<Country, Guid>
    {
        public CountryDataService(CountryClickerDbContext context) : base(context) { }

        public override void CreateMany(Country[] instances) => m_context.Countries.AddRange(instances);
        public override void DeleteMany(Country[] instances) => m_context.Countries.RemoveRange(instances);
        public override Country Get(Guid id) => m_context.Countries.Find(id);
        public override IEnumerable<Country> GetMany() => m_context.Countries;
        public override void UpdateMany(Country[] instances) => m_context.Countries.UpdateRange(instances);
    }
}
