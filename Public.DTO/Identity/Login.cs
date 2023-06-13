using System.ComponentModel.DataAnnotations;

namespace Public.DTO.Identity;

public class Login
{
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; } = default!;
    //[StringLength(maximumLength:128, MinimumLength = 5, ErrorMessage = "Password is too short")] 
    public string Password { get; set; } = default!;

}
