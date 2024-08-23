using System.ComponentModel.DataAnnotations;

public class LoginViewModel
{
    [Required]
    [Display(Name = "Username")]
    public required string Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public requiredstring Password { get; set; }

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
}
