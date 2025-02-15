﻿using Employee_Monitoring_System_API.Data;
using Employee_Monitoring_System_API.Models;
using Employee_Monitoring_System_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Employee_Monitoring_System_API.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public _Task Add(_Task task)
        {
            task.StartDate = task.StartDate.ToUniversalTime();
            task.EndDate = task.EndDate?.ToUniversalTime();
            task.CompletionDate = task.CompletionDate?.ToUniversalTime();
            _context.Tasks.Add(task);
            _context.SaveChanges();
            return task;
        }

        public _Task Delete(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
            }
            return task;
        }

        public _Task GetTask(int id)
        {
            return _context.Tasks.Find(id);
        }

        public IEnumerable<_Task> GetTasks()
        {
            return _context.Tasks.ToList();
        }

        public _Task Update(_Task taskChanges)
        {
            var task = _context.Tasks.Attach(taskChanges);
            task.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return taskChanges;
        }
    }
}
