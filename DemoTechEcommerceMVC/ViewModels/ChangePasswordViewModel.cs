﻿using System.ComponentModel.DataAnnotations;

namespace DemoTechEcommerceMVC.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "The {0} must be at {40} and at max {1} characters long")]
        [DataType(DataType.Password)]
        [Compare("ConfirmNewPassword", ErrorMessage = "Password does not match.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        public string ConfirmNewPassword { get; set; }
    }
}
