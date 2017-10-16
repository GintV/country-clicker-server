using System;
using CountryClicker.Domain;

namespace CountryClicker.API.Models.Get
{
    public class UserGetDto : IGetDto<User, Guid>
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
    }
}
