using System.ComponentModel.DataAnnotations;

namespace Demo_WebApp.Models.ViewModels.TodoViewModels
{
    public class TodoEditViewModel : TodoCreateViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Is Complete")]
        public bool IsComplete { get; set; }
    }
}
