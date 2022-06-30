using AutoMapper;
using Data.Repositories.Abstractions;
using Data.ViewModel;

namespace TodoApp.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repo;
        private readonly IMapper _mapper;

        public TaskService(ITaskRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task CreateTask(TaskViewModel task)
        {
            var taskToAdd = _mapper.Map<Data.Models.Task>(task);
            var added = await _repo.CreateTask(taskToAdd);
            
        }

        public async Task DeleteMultpleTasks(List<int> ids)
        {
            await _repo.DeleteMultpleTasks(ids);
        }

        public async Task<TaskViewModel> DeleteTaskById(int id)
        {
            var task = await _repo.DeleteTaskById(id);
            var returnTask = _mapper.Map<TaskViewModel>(task);
            return returnTask;
        }

        public async Task EditTask(TaskViewModel task)
        {
            var taskToEdit = _mapper.Map<Data.Models.Task>(task);
            await _repo.EditTask(taskToEdit);
        }

        public async Task<List<TaskViewModel>> FindByDate(DateTime date)
        {
            var tasks = await _repo.FindByDate(date);
            return _mapper.Map<List<TaskViewModel>>(tasks);
        }

        public async Task<List<TaskViewModel>> FindTasks(string searchWord)
        {
            var tasks = await _repo.FindTasks(searchWord);
            return _mapper.Map<List<TaskViewModel>>(tasks);
        }

        public async Task<List<TaskViewModel>> GetAllTasks()
        {
            var allTasks = await _repo.GetAllTasks();
            return _mapper.Map<List<TaskViewModel>>(allTasks);
        }

        public async Task<TaskViewModel> GetTaskById(int id)
        {
            var task = await _repo.GetTaskById(id);
            return _mapper.Map<TaskViewModel>(task);
        }
    }
}
