using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CountryClicker.Domain
{
    public class User : IEntity
    {
        // Table columns
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}