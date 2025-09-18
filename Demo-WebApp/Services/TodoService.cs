using Demo_WebApp.Data;
using Demo_WebApp.Models;
using Demo_WebApp.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Demo_WebApp.Services
{
    public class TodoService : ITodoService
    {
        private readonly ApplicationDbContext _context;

        public TodoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItem>> GetUserTodosAsync(string userId)
        {
            var result = await _context.TodoItems
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.CreatedDate)
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<TodoItem>> GetFilteredTodosAsync(string userId, string? searchTerm, Priority? priority, bool? isComplete)
        {
            var query = _context.TodoItems.Where(t => t.UserId == userId);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(t => t.Title.Contains(searchTerm) ||
                    (t.Description != null && t.Description.Contains(searchTerm)));
            }
            if (priority.HasValue)
            {
                query = query.Where(t => t.Priority == priority.Value);
            }
            if (isComplete.HasValue) 
            {
                query = query.Where(t => t.IsComplete == isComplete.Value);            
            }
            return await query
                .OrderByDescending(t => t.Priority)
                .ThenByDescending(t => t.CreatedDate)
                .ToListAsync();
        }
        public async Task<TodoItem?> GetTodoByIdAsync(int id, string userId)
        {
            return await _context.TodoItems
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
        }

        public async Task<TodoItem> CreateTodoAsync(TodoCreateViewModel model, string userId)
        {
            var todo = new TodoItem
            {
                Title = model.Title,
                Description = model.Description,
                Priority = model.Priority,
                DueDate = model.DueDate,
                UserId = userId,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };
            _context.TodoItems.Add(todo);
            await _context.SaveChangesAsync();
            return todo;
        }

        public async Task<bool> UpdateTodoAsync(int id, TodoEditViewModel model,  string userId)
        {
            var todo = await GetTodoByIdAsync(id, userId);
            if (todo == null) return false;

            todo.Title = model.Title;
            todo.Description = model.Description;
            todo.Priority = model.Priority;
            todo.DueDate = model.DueDate;
            todo.IsComplete = model.IsComplete;
            todo.UpdatedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTodoAsync(int id, string userId) 
        {
            var todo = await GetTodoByIdAsync(id, userId);
            if (todo == null) return false;

            todo.IsComplete = !todo.IsComplete;
            todo.UpdatedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task <TodoListViewModel> GetTodoStatisticsAsync(string userId)
        {
            var todos = await GetUserTodosAsync(userId);

            return new TodoListViewModel
            {
                TodoItems = todos,
                TotalItems = todos.Count(),
                CompletedItems = todos.Count(t => t.IsComplete),
                PendingItems = todos.Count(t => !t.IsComplete)
            };
        }

        public async Task<bool> ToggleCompleteAsync(int id, string userId)
        {
            var todo = await GetTodoByIdAsync(id, userId);
            if (todo == null) return false;

            todo.IsComplete = !todo.IsComplete;
            todo.UpdatedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
