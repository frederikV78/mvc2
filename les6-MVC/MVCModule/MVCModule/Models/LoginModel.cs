using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCModule.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please enter a username")]
        [DataType(DataType.Text)]
        [Display(Name = "Your username")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter a password")]
        [DataType(DataType.Password)]
        [Display(Name = "Your password")]
        public string Password { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "You're remembered")]
        public bool RememberMe { get; set; }

    }
}