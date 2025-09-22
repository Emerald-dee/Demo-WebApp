using Demo_WebApp.Models;
using Demo_WebApp.Models.ViewModels.TodoViewModels;

namespace Demo_WebApp.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoItem>> GetUserTodosAsync(string userId);
        Task<IEnumerable<TodoItem>> GetFilteredTodosAsync(string userId, string? searchTerm, Priority? priority, bool? isComplete);
        Task<TodoItem?> GetTodoByIdAsync(int id, string userId);
        Task<TodoItem> CreateTodoAsync(TodoCreateViewModel model, string userId);
        Task<bool> UpdateTodoAsync(int id, TodoEditViewModel model, string userId);
        Task<bool> DeleteTodoAsync(int id, string useId);
        Task<bool> ToggleCompleteAsync(int id, string userId);
        Task<TodoListViewModel> GetTodoStatisticsAsync(string userId);

    }
}
