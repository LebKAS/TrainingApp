using System.ComponentModel.DataAnnotations;

namespace DatingApp.api.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
          public string Username { get; set; }  

          [Required]
          [StringLength(20,MinimumLength =5, ErrorMessage ="Length between 5 and 20")]
          public string Password { get; set; }  
    }
}