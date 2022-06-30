using AutoMapper;
using Data.Repositories.Abstractions;
using Data.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoApp.Services;

namespace TodoApp.Pages
{
    [BindProperties(SupportsGet = true)]
    public class IndexModel : PageModel
    {
       
        private readonly ITaskService _service;
        public TaskViewModel Task { get; set; }
        public string SearchQuery { get; set; }
        public DateTime Date { get; set; } = DateTime.Now.Date;

        public IndexModel(ITaskService service)
        {
            _service = service;
        }
        
        public IList<TaskViewModel> Tasks { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if(string.IsNullOrEmpty(SearchQuery))
            {
                Tasks = await _service.GetAllTasks();
            }
            else
            {
                Tasks = await _service.FindTasks(SearchQuery);
            }
            
           
            return Page();
        }

        public async Task<IActionResult> OnPostSearchByDate()
        {
            Tasks = await _service.FindByDate(Date);
            return Page();
        }

        public async Task<IActionResult> OnPostAdd()
        {
            if(!string.IsNullOrEmpty(Task.TodoTask))
            {
                await _service.CreateTask(Task);
            }
            
            Task = new TaskViewModel();

            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            await _service.DeleteTaskById(id);

            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostDeleteSelected()
        {
            var ids = new List<int>();
            foreach(var task in Tasks)
            {
                if(task.Check)
                    ids.Add(task.Id);
            }
            await _service.DeleteMultpleTasks(ids);
            return RedirectToPage("Index");
        }

    }
}