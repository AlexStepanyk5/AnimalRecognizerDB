using System;
using System.ComponentModel.DataAnnotations;

namespace AnimalRecognizers.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
