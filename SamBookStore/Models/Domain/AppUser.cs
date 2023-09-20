using Microsoft.AspNetCore.Identity;

namespace SamBookStore.Models.Domain
{
    public class AppUser:IdentityUser
    {
        public string Address { get; set; }
    }
}