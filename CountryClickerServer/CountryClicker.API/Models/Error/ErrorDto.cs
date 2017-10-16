using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryClicker.API.Models.Error
{
    public class ErrorDto
    {
        public virtual string Error { get; }
        public string ErrorDescription { get; protected set; }
    }
}
