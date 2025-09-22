using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Demo_WebApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name ="First Name")]
        public string? FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
    }
}
