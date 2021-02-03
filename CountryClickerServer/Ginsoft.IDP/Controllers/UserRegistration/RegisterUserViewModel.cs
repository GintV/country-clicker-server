using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ginsoft.IDP.Controllers.UserRegistration
{
    public class RegisterUserViewModel
    {
        // credentials       
        [MaxLength(100)]
        public string Username { get; set; }

        [MaxLength(100)]
        public string Password { get; set; }

        // claims 
        [Required]
        [MaxLength(150)]
        public string Email { get; set; }

        [Required]
        [MaxLength(36)]
        public string Country { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nickname { get; set; }

        public SelectList CountryCodes { get; set; } = new SelectList(new[]
        {
            new { Id = "5d6f4a4b-a275-4db7-93a1-08d53cdf8020", Value = "Belgium" },
            new { Id = "4ac6abf6-19f3-40bd-939f-08d53cdf8020", Value = "India" },
            new { Id = "414ec2cd-0452-4304-93a0-08d53cdf8020", Value = "Lithuania" },
            new { Id = "34313dfe-c69d-4b58-93a2-08d53cdf8020", Value = "United States of America" }
        }, "Id", "Value");

        public string ReturnUrl { get; set; }

        public string Provider { get; set; }
        public string ProviderUserId { get; set; }
        public bool IsExternalProvider { get { return !string.IsNullOrEmpty(Provider); } }

    }
}
