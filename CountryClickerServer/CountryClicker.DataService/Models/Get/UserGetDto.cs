using CountryClicker.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountryClicker.DataService.Models.Get
{
    public class UserGetDto : IGetDto<User, Guid>
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
    }
}
