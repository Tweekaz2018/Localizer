//using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class LoginPageModel
    {
        [Display(Name ="Login")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}