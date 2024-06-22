using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace MyBlog.Models
{
    public class ExternalLoginsViewModel
    {
        public IList<UserLoginInfo>? CurrentLogins { get; set; }
        public IList<AuthenticationScheme>? OtherLogins { get; set; }
        public bool ShowRemoveButton { get; set; }
        public bool Is2faEnabled { get; set; }
        public bool HasPassword { get; set; }
    }
}
