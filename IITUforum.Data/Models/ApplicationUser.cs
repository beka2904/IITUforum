using Microsoft.AspNetCore.Identity;
using System;

namespace IITUforum.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string UserDescription { get; set; }
        public string ProfileImageUrl { get; set; }
        public int Rating { get; set; }
        public bool isAdmin { get; set; }
        public bool isActive { get; set; }
        public DateTime MemberSince { get; set; }
    }
}
