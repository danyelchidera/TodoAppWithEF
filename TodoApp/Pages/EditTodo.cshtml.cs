using AutoMapper;
using Data.Repositories.Abstractions;
using Data.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TodoApp.Pages
{
    [BindProperties]
    public class EditTodoModel : PageModel
    {
        private readonly ITaskRepository _repo;

        private readonly IMapper _mapper;
        public TaskViewModel Task { get; set; }
        public EditTodoModel(ITaskRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            var task = await _repo.GetTaskById(id);
            Task = _mapper.Map<TaskViewModel>(task);
            return Page();

        }

        public async Task<IActionResult> OnPost()
        {
            var task = _mapper.Map<Data.Models.Task>(Task);   
            await _repo.EditTask(task);
            return RedirectToPage("Index");
        }
    }
}
