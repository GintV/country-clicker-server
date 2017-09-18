using CountryClicker.Data;
using CountryClicker.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountryClicker.DataService
{
    public class ContinentDataService : DataService<Continent, Guid>
    {
        public ContinentDataService(CountryClickerDbContext context) : base(context) { }

        public override void CreateMany(Continent[] instances) => m_context.Continents.AddRange(instances);
        public override void DeleteMany(Continent[] instances) => m_context.Continents.RemoveRange(instances);
        public override Continent Get(Guid id) => m_context.Continents.Find(id);
        public override IEnumerable<Continent> GetMany() => m_context.Continents;
        public override void UpdateMany(Continent[] instances) => m_context.Continents.UpdateRange(instances);
    }
}
