using CountryClicker.Data;
using CountryClicker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace CountryClicker.DataService
{
    public class UserDataService : DataService<User, Guid>
    {
        public UserDataService(CountryClickerDbContext context) : base(context) { }

        public override User Get(Guid id) => Context.Users.Find(id);
        public override IQueryable<User> GetMany() => Context.Users;
        public override IQueryable<User> GetManyFilter(params (string column, string value)[] columnValuePairs) => Context.Users.
            FromSql($"SELECT * FROM Users WHERE {CombineFilter(columnValuePairs)}".ToString());
        public override bool AreRelationshipsValid(User instance) => true;
    }
}
