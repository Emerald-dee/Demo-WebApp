using System.ComponentModel.DataAnnotations;

namespace Demo_WebApp.Models.ViewModels.TodoViewModels
{
    public class TodoCreateViewModel
    {
        [Required (ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }

        [Display(Name = "Priority")]
        public Priority Priority { get; set; } = Priority.Medium;

        [Display(Name = "Due Date")]
        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }
    }
}
