using Data.ViewModel;

namespace TodoApp.Services
{
    public interface ITaskService
    {
        Task CreateTask(TaskViewModel task);
        Task<TaskViewModel> DeleteTaskById(int id);
        Task<List<TaskViewModel>> GetAllTasks();
        Task<TaskViewModel> GetTaskById(int id);
        Task EditTask(TaskViewModel task);
        Task<List<TaskViewModel>> FindTasks(string searchWord);
        Task<List<TaskViewModel>> FindByDate(DateTime date);
        Task DeleteMultpleTasks(List<int> ids);
    }
}
