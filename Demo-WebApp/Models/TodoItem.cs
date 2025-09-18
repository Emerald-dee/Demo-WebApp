using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo_WebApp.Models
{
    public class TodoItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, 
            ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }

        [Display(Name = "Is Complete")]
        public bool IsComplete { get; set; }

        [Display(Name = "Priority")]
        public Priority Priority { get; set; } = Priority.Medium;

        [Display(Name = "Due date")]
        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Display(Name = "Updated Date")]
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        [Required]
        public string UserId { get; set; } = String.Empty;
        public virtual ApplicationUser? User { get; set; }
    }
    public enum Priority
    {
        Low = 1,
        Medium = 2,
        High = 3,
        Urgent = 4,
    }
}
