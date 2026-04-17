using Microsoft.AspNetCore.Identity;

namespace ReactApp1.Server.Models.Project2Exercise
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }=string.Empty;
    }
}
