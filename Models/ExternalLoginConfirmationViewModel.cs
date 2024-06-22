using Microsoft.AspNetCore.Identity;

namespace MyBlog.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        public string? ReturnUrl { get; set; }
        public ExternalLoginInfo? Info { get; set; }

        // Additional fields you might need for user input:
        public string? Email { get; set; }
        // ... potentially other fields
    }
}
