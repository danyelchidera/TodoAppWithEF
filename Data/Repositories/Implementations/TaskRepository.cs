using Data.Repositories.Abstractions;
using Data.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Implementations
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;

        }

        public async Task<Models.Task> CreateTask(Models.Task task)
        {
            task.Date = DateTime.Now;
            var res = _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return res.Entity;
        }

        public async Task DeleteMultpleTasks(List<int> ids)
        {
            var entities = await _context.Tasks.Where(x => ids.Contains(x.Id)).ToListAsync();

            _context.Tasks.RemoveRange(entities);

            await _context.SaveChangesAsync();
        }

        public async Task<Models.Task> DeleteTaskById(int id)
        {
            var entity = _context.Tasks.Find(id);
            var res = _context.Tasks.Remove(entity);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task EditTask(Models.Task task)
        {
            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();
           
        }

        public async Task<List<Models.Task>> FindByDate(DateTime date)
        {
            var res = await _context.Tasks.Where(t => t.Date.Date == date.Date).ToListAsync();
            return res;
        }

        public async Task<List<Models.Task>> FindTasks(string searchWord)
        {
            var res = await _context.Tasks.Where(t => t.TodoTask.Contains(searchWord)).ToListAsync();
            return res;
        }

        public async Task<List<Models.Task>> GetAllTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<Models.Task> GetTaskById(int id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
