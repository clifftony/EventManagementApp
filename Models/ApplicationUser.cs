using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace EventManagementApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
