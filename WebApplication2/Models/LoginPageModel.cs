//using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using WebApplication2.Attributes;

namespace WebApplication2.Models
{
    public class LoginPageModel
    {
        [LocalizedDisplay(Name ="Login")]
        // от 1 до 15 букв
        [LocalizedRegularExpression(@"^[a-zA-Z''-'\s]{1,15}$",ErrorMessage = "Not valid")]
        public string Login { get; set; }
        [LocalizedRequired(ErrorMessage = "Required")]
        [LocalizedDisplay(Name = "Password")]
        public string Password { get; set; }
    }
}