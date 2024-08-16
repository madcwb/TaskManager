using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.Models;
using TaskManager.Repository.Interface;

namespace TaskManager.Repository.Services
{
    public class TaskListRepository : ITaskListRepository
    {
        private readonly AppDbContext _context;

        public TaskListRepository(AppDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<TaskList>> GetTaskList()
        {
            return await _context.TaskList.ToListAsync();
        }

        public async Task<TaskList> GetTaskList(int taskId)
        {
            var task = await _context.TaskList.FindAsync(taskId);

            return task;
        }

        public async Task<TaskList> AddTaskList(TaskList tasklist)
        {
            var result = await _context.TaskList.AddAsync(tasklist);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<TaskList> UpdateTaskList(TaskList tasklist)
        {
            var task = await _context.TaskList.SingleOrDefaultAsync(e => e.Id == tasklist.Id);

            if (task != null)
            {
                task.Description = tasklist.Description;

                _context.Entry(task).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return task;
            }
            return null;
        }

        public async void DeleteTaskList(int taskId)
        {
            var taskList = await _context.TaskList.FindAsync(taskId);
            if (taskList == null)
            {
                return ;
            }

            _context.TaskList.Remove(taskList);
            _context.SaveChanges();
        }
    }
}
