using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTO;

public class RegisterDTO
{
    [Required]
    public string DisplayName { get; set; } ="";

    [Required]      
    [EmailAddress]
    public string Email { get; set; } = "";

    [Required]
    [StringLength(16, MinimumLength = 6)]
    public string Password { get; set; } = "";
}
