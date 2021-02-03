using System.ComponentModel.DataAnnotations;
using CountryClicker.Domain;

namespace CountryClicker.API.Models.Update
{
    public class ContinentUpdateDto : IUpdateDto<Continent>
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public long Score { get; set; }
    }
}
