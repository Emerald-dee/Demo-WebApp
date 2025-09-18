namespace Demo_WebApp.Models.ViewModels
{
    public class TodoListViewModel
    {
        public IEnumerable<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
        public string SearchTerm { get; set; } = string.Empty;
        public Priority? FilterPriority { get; set; }
        public bool? FilterCompleted { get; set; }
        public int? TotalItems { get; set; }
        public int? CompletedItems { get; set; }
        public int? PendingItems { get; set; }
    }
}
