using Demo_WebApp.Models;
using Demo_WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Demo_WebApp.Models.ViewModels;

using Microsoft.AspNetCore.Http.HttpResults;
using Demo_WebApp.Models.ViewModels.TodoViewModels;

namespace Demo_WebApp.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        private readonly ITodoService _todoService;
        private readonly UserManager<ApplicationUser> _userManager;

        public TodoController(ITodoService todoService, UserManager<ApplicationUser> userManager)
        {
            _todoService = todoService;
            _userManager = userManager;
        }

        private string GetCurrentUserId()
        {
            return _userManager.GetUserId(User) ?? string.Empty;
        }

        public async Task<IActionResult> Index(string? search, Priority? priority, bool? completed)
        {
            var userId = GetCurrentUserId();
            var todos = await _todoService.GetFilteredTodosAsync(userId, search, priority, completed);
            var stats = await _todoService.GetTodoStatisticsAsync(userId);

            var viewModel = new TodoListViewModel
            {
                TodoItems = todos,
                SearchTerm = search ?? string.Empty,
                FilterPriority = priority,
                FilterCompleted = completed,
                TotalItems = stats.TotalItems,
                CompletedItems = stats.CompletedItems,
                PendingItems = stats.PendingItems
            };
            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Create(TodoCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = GetCurrentUserId();
                var todo = await _todoService.CreateTodoAsync(model, userId);
                TempData["Success"] = "Todo item, created successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var userId = GetCurrentUserId();
            var todo = await _todoService.GetTodoByIdAsync(id, userId);

            if (todo == null)
            {
                return NotFound();
            }
            var viewModel = new TodoEditViewModel
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                Priority = todo.Priority,
                DueDate = todo.DueDate,
                IsComplete = todo.IsComplete
            };

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TodoEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var userId = GetCurrentUserId();
                var success = await _todoService.UpdateTodoAsync(id, model, userId);

                if (success)
                {
                    TempData["Success"] = "Todo item updated successfully";
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var userId = GetCurrentUserId();
            var todo = await _todoService.GetTodoByIdAsync(id, userId);

            if (todo == null) { return NotFound(); }
            return View(todo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleComplete(int id)
        {
            var userId = GetCurrentUserId();
            var success = await _todoService.ToggleCompleteAsync(id, userId);
            if (!success)
            {
                TempData["Error"] = "Todo item not found";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
