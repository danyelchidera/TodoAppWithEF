
using Data.ViewModel;

namespace Data.Repositories.Abstractions
{
    public interface ITaskRepository
    {
        Task<Models.Task> CreateTask(Models.Task task);
        Task<Models.Task> DeleteTaskById(int id);
        Task<List<Models.Task>> GetAllTasks();
        Task<Models.Task> GetTaskById(int id);
        Task EditTask(Models.Task task);
        Task<List<Models.Task>> FindTasks(string searchWord);
        Task<List<Models.Task>> FindByDate(DateTime date);
        Task DeleteMultpleTasks(List<int> ids);
    }
}
