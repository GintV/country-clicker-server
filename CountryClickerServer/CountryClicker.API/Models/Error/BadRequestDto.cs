using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryClicker.API.Models.Error
{
    public class BadRequestDto : ErrorDto
    {
        public override string Error => "Error 400 Bad Request";

        public static BadRequestDto InvalidData => new BadRequestDto { ErrorDescription = "Model contains field with invalid values." };
    }
}
