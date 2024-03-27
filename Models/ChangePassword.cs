using System;
using MyBlog.Models;
using System.ComponentModel.DataAnnotations;

  namespace MyBlog.Models
  {
    public class ChangePasswordViewModel
      {
        [Required(ErrorMessage = "Current password is required")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New password is required")]
        [StringLength(100, ErrorMessage = "The password must be at least 6 characters long", MinimumLength = 6)]
        public string NewPassword { get; set; }

        [Compare(nameof(NewPassword), ErrorMessage = "The new password and confirmation password do not match")]
        public string ConfirmPassword { get; set; }
      }
  }
